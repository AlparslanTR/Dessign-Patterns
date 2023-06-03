using Microsoft.AspNetCore.Identity;

namespace Base.Main.Models
{
    public class Customer:IdentityUser
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
