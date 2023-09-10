using Base.Main.Models.Data;
using Microsoft.EntityFrameworkCore;
using WebApp.DecoratorPattern.Models;

namespace WebApp.DecoratorPattern.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _Context;

        public ProductRepository(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<Product>Add(Product product)
        {
            await _Context.Products.AddAsync(product);
            await _Context.SaveChangesAsync();
            return product; 
        }

        public async Task DeleteById(int id)
        {
            var findProduct = await _Context.Products.FindAsync(id);
            if (findProduct is not null)
            {
                _Context.Products.Remove(findProduct);
                await _Context.SaveChangesAsync();
            }
            else { throw new NotImplementedException("Seçtiğiniz Id Bulunamadı",null); }
        }

        public async Task<List<Product>> GetAll()
        {
            return await _Context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetAll(string userId)
        {
            return await _Context.Products.Where(x=>x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _Context.Products.FindAsync(id);
        }

        public async Task Update(Product product)
        {
            _Context.Products.Update(product);
            await _Context.SaveChangesAsync();
        }
    }
}
