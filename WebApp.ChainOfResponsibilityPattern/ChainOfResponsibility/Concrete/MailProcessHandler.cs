using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using WebApp.ChainOfResponsibilityPattern.ChainOfResponsibility.Abstract;

namespace WebApp.ChainOfResponsibilityPattern.ChainOfResponsibility.Concrete
{
    public class MailProcessHandler : ProcessHandler
    {
        private readonly string _fileName;
        private readonly string _toEmail;

        public MailProcessHandler(string fileName, string toEmail)
        {
            _fileName = fileName;
            _toEmail = toEmail;
        }

        public override object handle(object o)
        {
            var zipMemoryStream = o as MemoryStream;
            zipMemoryStream.Position = 0;

            var mailMessage = new MailMessage();
            var smtp = new SmtpClient("77.245.159.27");

            mailMessage.From = new MailAddress("deneme@alparslanakbas.org.tr");
            mailMessage.To.Add(new MailAddress(_toEmail));
            mailMessage.Subject = "Zip Dosyası";
            mailMessage.Body = @"<p>Zip Dosyanız Ektedir.</p>";

            Attachment attachment = new Attachment(zipMemoryStream, _fileName, MediaTypeNames.Application.Zip);
            mailMessage.Attachments.Add(attachment);

            mailMessage.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("deneme@alparslanakbas.org.tr", "alparslan123");
            smtp.Send(mailMessage);

            return base.handle(null);
        }
    }
}
