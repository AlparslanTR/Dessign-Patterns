using System.Text;

namespace WebApp.TemplatePattern.UserCards
{
    public class UserCardTemplateRegister : UserCardTemplateDefault
    {
        // Kayıt olan kullanıcıların gösterilecek olan alan
        protected override string SetFooter()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<a href='#' class='btn btn-primary'>Mesaj Gönder</a>");
            stringBuilder.Append("<a href='#' class='btn btn-info'>Profil Gör</a>");
            return stringBuilder.ToString();    
        }

        protected override string SetPicture()
        {
            return $@"<img class='card-img-top' src='{Customer.ImageUrl}'>";
        }
    }
}
