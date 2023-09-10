using WebApp.DecoratorPattern.Models;

namespace WebApp.DecoratorPattern.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<List<Product>> GetAll(string userId);
        Task<Product> GetById(int id);
        Task<Product>Add(Product product);
        Task Update(Product product);
        Task DeleteById(int id);


    }
}
