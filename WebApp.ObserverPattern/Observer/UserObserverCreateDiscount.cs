using Base.Main.Models;
using Base.Main.Models.Data;
using WebApp.ObserverPattern.Models;

namespace WebApp.ObserverPattern.Observer
{
    public class UserObserverCreateDiscount : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateDiscount(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(Customer customer)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Discounts.Add(new Models.Discount()
            {
                Rate = 15,
                UserId = customer.Id,
            });
            context.SaveChanges();

            
            Discount discount = new();
            discount.Rate = 15;
            logger.LogInformation($"{customer.UserName} için % {discount.Rate} indirim uygulandı.");
        }
    }
}
