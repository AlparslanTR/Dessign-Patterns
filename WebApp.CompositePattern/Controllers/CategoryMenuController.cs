using Base.Main.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.CompositePattern.Composite;
using WebApp.CompositePattern.Models;

namespace WebApp.CompositePattern.Controllers
{
    [Authorize]
    public class CategoryMenuController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryMenuController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var getCategories = await _context.Categories.Include(x => x.Books).Where(x => x.UserId == user).OrderBy(x => x.Id).ToListAsync();

            var menu = GetMenues(getCategories, new Category { Name = "TopCategory", Id = 0 }, new BookComposite(0, "TopMenu"));

            ViewBag.Menu = menu;
            ViewBag.selectList = menu._components.SelectMany(x => ((BookComposite)x).GetSelectListItems(""));

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int categoryId, string bookName)
        {
            await _context.Books.AddAsync(new Book { CategoryId = categoryId, Name = bookName });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public BookComposite GetMenues(List<Category> categories, Category topCategory, BookComposite topBook, BookComposite last = null)
        {
            categories.Where(x => x.ReferenceId == topCategory.Id).ToList().ForEach(categoryItem =>
            {
                var bookComposite = new BookComposite(categoryItem.Id, categoryItem.Name);

                categoryItem.Books.ToList().ForEach(bookItem =>
                {
                    bookComposite.Add(new BookComponent(bookItem.Id, bookItem.Name));
                });

                if (last is not null)
                {
                    last.Add(bookComposite);
                }
                else
                {
                    topBook.Add(bookComposite);
                }

                GetMenues(categories, categoryItem, topBook, bookComposite);
            });
            return topBook;
        }
    }
}
