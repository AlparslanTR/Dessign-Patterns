using Microsoft.Extensions.Caching.Memory;
using WebApp.DecoratorPattern.Models;
using WebApp.DecoratorPattern.Repositories;

namespace WebApp.DecoratorPattern.Decarators
{
    public class ProductCacheDecorator : BaseProductRepositoryDecorator
    {
        private readonly IMemoryCache _memoryCache;
        private const string CacheName = "Products";
        public ProductCacheDecorator(IProductRepository repository, IMemoryCache memoryCache) : base(repository)
        {
            _memoryCache = memoryCache;
        }

        private async Task UpdateCache()
        {
            _memoryCache.Set(CacheName, await base.GetAll());
        }

        public async override Task<List<Product>> GetAll()
        {
            if (_memoryCache.TryGetValue(CacheName,out List<Product> cacheProduct))
            {
                return cacheProduct;
            }
            await UpdateCache();
            return _memoryCache.Get<List<Product>>(CacheName);
        }

        //

        public override async Task<List<Product>> GetAll(string userId)
        {
            var product = await GetAll();
            return product.Where(x=>x.UserId == userId).ToList();
        }

        //

        public override async Task<Product> Add(Product product)
        {
            await base.Add(product);
            await UpdateCache();
            return product;
        }

        //

        public override async Task Update(Product product)
        {
            await base.Update(product);
            await UpdateCache();
        }

        //

        public override async Task DeleteById(int id)
        {
            await base.DeleteById(id);
            await UpdateCache();
        }


    }
}
