using WebApp.DecoratorPattern.Models;
using WebApp.DecoratorPattern.Repositories;

namespace WebApp.DecoratorPattern.Decarators
{
    public class BaseProductRepositoryDecorator : IProductRepository
    {
        public readonly IProductRepository _repository;

        public BaseProductRepositoryDecorator(IProductRepository repository)
        {
            _repository = repository;
        }

        public virtual Task<Product> Add(Product product)
        {
            return _repository.Add(product);
        }

        public virtual async Task DeleteById(int id)
        {
            await _repository.DeleteById(id);
        }

        public virtual async Task<List<Product>> GetAll()
        {
            return await _repository.GetAll();
        }

        public virtual async Task<List<Product>> GetAll(string userId)
        {
            return await _repository.GetAll(userId);  
        }

        public virtual async Task<Product> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public virtual async Task Update(Product product)
        {
            await _repository.Update(product);
        }
    }
}
