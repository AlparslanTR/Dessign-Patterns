using WebApp.StrategyPattern.Models;

namespace WebApp.StrategyPattern.Repositories
{
    public interface IProductRepo
    {
        Task<Product> GetById(string id);
        Task<List<Product>> GetByUserId(string userId);
        Task<Product> Add(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}
