using Base.Main.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ChainOfResponsibilityPattern.ChainOfResponsibility.Concrete;
using WebApp.ChainOfResponsibilityPattern.Models;

namespace WebApp.ChainOfResponsibilityPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> SendMail()
        {
            var getProdcuts = await _context.Products.ToListAsync();

            ExcelProcessHandler<Product> excelProcessHandler = new();
            ZipProcessHandler<Product> zipProcessHandler = new();
            MailProcessHandler mailProcessHandler = new("Ürünler.zip", "bybluestht@gmail.com");

            excelProcessHandler.SetNext(zipProcessHandler).SetNext(mailProcessHandler);
            excelProcessHandler.handle(getProdcuts);

            return View(nameof(Index));
        }

        public async Task<IActionResult> Products() => View (await _context.Products.ToListAsync());
    }
}
