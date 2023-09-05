using Base.Main.Models;
using System.Net;
using System.Net.Mail;

namespace WebApp.ObserverPattern.Observer
{
    public class UserObserverCreateEmail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(Customer customer)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateEmail>>();

            var mailMessage = new MailMessage();
            var smtp = new SmtpClient("77.245.159.27");

            mailMessage.From = new MailAddress("deneme@alparslanakbas.org.tr");
            mailMessage.To.Add(new MailAddress(customer.Email));
            mailMessage.Subject = "Siteye Hoş Geldiniz.";
            mailMessage.Body = @"<p>Yeni Kayıt Olan Üyelere Özel %15 İndirim Hesabınıza Tanımlandı</p>";
            mailMessage.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("deneme@alparslanakbas.org.tr", "alparslan123");
            smtp.Send(mailMessage);

            logger.LogInformation($"{customer.UserName} Adlı Kişinin Mail Adresine Bilgi Mesajı Gönderilmiştir.");
        }
    }
}
