using Base.Main.Models;
using System.Text;

namespace WebApp.TemplatePattern.UserCards
{
    public abstract class UserCardTemplateDefault
    {
        // Kemik Yapı
        protected Customer Customer { get; set; }
        public void SetUser(Customer customer)
        {
            Customer = customer;
        }
        public string Build()
        {
            if (Customer == null)  throw new Exception(nameof(Customer));

            var stringBuilder= new StringBuilder();
            stringBuilder.Append("<div class='card'>");

            stringBuilder.Append(SetPicture());
            stringBuilder.Append($@"<div class='card-body'>
            <h5>{Customer.UserName}</h5> 
            <p>{Customer.Description}</p>
            ");
            stringBuilder.Append(SetFooter());
            stringBuilder.Append("</div>");

            stringBuilder.Append("</div>");

            return stringBuilder.ToString();
        }
        protected abstract string SetFooter();
        protected abstract string SetPicture();
    }
}
