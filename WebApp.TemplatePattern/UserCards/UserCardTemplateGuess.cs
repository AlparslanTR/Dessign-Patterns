namespace WebApp.TemplatePattern.UserCards
{
    public class UserCardTemplateGuess : UserCardTemplateDefault
    {
        // Üye olmayan kullanıcılara gösterilecek alan.

        protected override string SetFooter()
        {
            return string.Empty;
        }

        protected override string SetPicture()
        {
            return $"<img class='card-img-top' src='/UserPictures/defaultUser.png'>";
        }
    }
}
