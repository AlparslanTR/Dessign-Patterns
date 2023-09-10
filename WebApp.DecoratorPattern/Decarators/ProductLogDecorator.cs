using Base.Main.Models;
using Microsoft.AspNetCore.Identity;
using WebApp.DecoratorPattern.Models;
using WebApp.DecoratorPattern.Repositories;

namespace WebApp.DecoratorPattern.Decarators
{
    public class ProductLogDecorator : BaseProductRepositoryDecorator
    {
        private readonly ILogger<ProductLogDecorator> _logger;
        private readonly UserManager<Customer> _userManager;
        public ProductLogDecorator(IProductRepository repository, ILogger<ProductLogDecorator> logger, UserManager<Customer> userManager) : base(repository)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public override Task<List<Product>> GetAll()
        {
            _logger.LogInformation("Ürünler Listelendi");
            return base.GetAll();
        }

        public override async Task<List<Product>> GetAll(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            _logger.LogInformation($"{user.UserName} Adlı Kullanıcının Ürünleri Getirildi.!");
            return await base.GetAll(userId);
        }

        public override Task<Product> Add(Product product)
        {
            _logger.LogInformation($"{product.Name} Adlı Ürün Eklendi.!");
            return base.Add(product);
        }

        public override Task Update(Product product)
        {
            _logger.LogInformation($"{product.Name} Ürünü Güncellendi.!");
            return base.Update(product);
        }

        public override async Task DeleteById(int id)
        {
            var product = await GetById(id);
            _logger.LogInformation($"{product.Name} Adlı Ürün Silinmiştir.!");
            await base.DeleteById(id);
        }


    }
}
