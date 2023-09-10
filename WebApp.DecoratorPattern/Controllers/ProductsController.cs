using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Base.Main.Models.Data;
using WebApp.DecoratorPattern.Models;
using Microsoft.AspNetCore.Authorization;
using WebApp.DecoratorPattern.Repositories;
using System.Security.Claims;

namespace WebApp.DecoratorPattern.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        // GET: Products User Id
        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.First(x=>x.Type==ClaimTypes.NameIdentifier).Value;
            if (userId is null)
            {
                return BadRequest("Kullanıcı Sistemde Değil");
            }
            return View(await _productRepository.GetAll(userId));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return BadRequest("Geçersiz Id");
            }

            if (_productRepository is null)
            {
                return StatusCode(500, "Sunucu Hatası");
            }
            else
            {
                var product = await _productRepository.GetById(id.Value);
                return View(product);
            }       
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
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                product.UserId = userId;
                await _productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null)
            {
                return BadRequest("Geçersiz Id");
            }

            if (_productRepository is null)
            {
                return StatusCode(500, "Sunucu Hatası");
            }

            var product = await _productRepository.GetById(id.Value);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Stock,UserId")] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Geçersiz Id");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productRepository.Update(product);
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest("Geçersiz Id");
            }

            if (_productRepository is null)
            {
                return StatusCode(500, "Sunucu Hatası");
            }
            var product = await _productRepository.GetById(id.Value);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_productRepository is null)
            {
                return StatusCode(500, "Sunucu Hatası");
            }
            var product = await _productRepository.GetById(id);
            if (product is not null)
            {
                await _productRepository.DeleteById(product.Id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _productRepository.GetById(id) is not null;
        }
    }
}
