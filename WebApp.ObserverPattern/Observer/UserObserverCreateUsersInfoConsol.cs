using Base.Main.Models;

namespace WebApp.ObserverPattern.Observer
{
    public class UserObserverCreateUsersInfoConsol : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateUsersInfoConsol(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(Customer customer)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateUsersInfoConsol>>();
            logger.LogInformation($"{customer.Id}'li {customer.UserName} Adında Kullanıcı Oluşturuldu.");
        }
    }
}
