using Base.Main.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TemplatePattern.UserCards
{
    public class UserCardTagHelper : TagHelper
    {
        public Customer Customer { get; set; }

        private readonly IHttpContextAccessor _contextAccessor;

        public UserCardTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            UserCardTemplateDefault userCardTemplateDefault;
            if(_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                userCardTemplateDefault = new UserCardTemplateRegister();

            }
            else
            {
                userCardTemplateDefault= new UserCardTemplateGuess();
            }
            userCardTemplateDefault.SetUser(Customer);

            output.Content.SetHtmlContent(userCardTemplateDefault.Build());
        }
    }
}
