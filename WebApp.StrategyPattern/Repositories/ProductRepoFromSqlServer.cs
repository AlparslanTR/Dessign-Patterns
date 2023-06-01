using Base.Main.Models.Data;
using Microsoft.EntityFrameworkCore;
using WebApp.StrategyPattern.Models;

namespace WebApp.StrategyPattern.Repositories
{
    public class ProductRepoFromSqlServer : IProductRepo
    {
        private readonly AppDbContext _context;

        public ProductRepoFromSqlServer(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Add(Product product)
        {
            product.Id =Guid.NewGuid().ToString();
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Delete(Product product)
        {
             _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetById(string id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetByUserId(string userId)
        {
            return await _context.Products.Where(x=>x.UserId == userId).ToListAsync();
        }

        public async Task Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
