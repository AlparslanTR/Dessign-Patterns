using Base.Main.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.StrategyPattern.Models;

namespace WebApp.StrategyPattern.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;

        public SettingsController(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Settings settings = new(); 
            if (User.Claims.Where(x=>x.Type==Settings.ClaimDatabaseType).FirstOrDefault()!=null)
            {
                settings.DatabaseType =(EDatabaseType) int.Parse(User.Claims.First(x=>x.Type== Settings.ClaimDatabaseType).Value);
            }
            else
            {
                settings.DatabaseType = settings.GetDefaultDatabaseType;
            }
            return View(settings);
        }
        [HttpPost]
        public async Task <IActionResult> ChangeDatabase(int databaseType)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var newClaim = new Claim(Settings.ClaimDatabaseType,databaseType.ToString());
            var claims = await _userManager.GetClaimsAsync(user);
            var hasDataBaseTypeClaim = claims.FirstOrDefault(x => x.Type == Settings.ClaimDatabaseType);

            if (hasDataBaseTypeClaim!=null)
            {
                await _userManager.ReplaceClaimAsync(user, hasDataBaseTypeClaim, newClaim);
            }
            else
            {
                await _userManager.AddClaimAsync(user, newClaim);
            }

            await _signInManager.SignOutAsync();
            var authResult= await HttpContext.AuthenticateAsync();
            await _signInManager.SignInAsync(user, authResult.Properties);
            return RedirectToAction(nameof(Index));
        }
    }
}
