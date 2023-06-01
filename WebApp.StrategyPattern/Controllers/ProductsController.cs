using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Base.Main.Models.Data;
using WebApp.StrategyPattern.Models;
using WebApp.StrategyPattern.Repositories;
using Microsoft.AspNetCore.Identity;
using Base.Main.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.StrategyPattern.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepo _productRepo;
        private readonly UserManager<Customer> _userManager;
        public ProductsController(IProductRepo productRepo, UserManager<Customer> userManager)
        {
            _productRepo = productRepo;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            
            return View(await _productRepo.GetByUserId(user.Id));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _productRepo.GetById(id) == null)
            {
                return NotFound();
            }

            var product = await _productRepo.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock,UserId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                product.UserId = user.Id; 
               await _productRepo.Add(product);
               return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _productRepo.GetById(id) == null)
            {
                return NotFound();
            }

            var product = await _productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Price,Stock,UserId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productRepo.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _productRepo.GetById(id) == null)
            {
                return NotFound();
            }

            var product = await _productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_productRepo.GetById(id) == null)
            {
                return Problem("Ürün Bulunamadı.!");
            }
            var product = await _productRepo.GetById(id);
            if (product != null)
            {
               await _productRepo.Delete(product);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
          return _productRepo.GetById(id) !=null;
        }
    }
}
