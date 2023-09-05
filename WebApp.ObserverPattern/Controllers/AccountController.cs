using Base.Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ObserverPattern.Observer;

namespace Base.Main.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly UserSubject _userSubject;


        public AccountController(UserManager<Customer> userManager, SignInManager<Customer> signInManager, UserSubject userSubject)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userSubject = userSubject;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) { return View("Error"); }
            var signIn = await _signInManager.PasswordSignInAsync(user, password, true, true);

            if (signIn.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.Index), "Account");
            }

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login),"Account");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string Username, string Password, string Email)
        {
            var User = new Customer()
            {
                UserName = Username,
                Email = Email,
                PasswordHash = Password
            };
            var identityResult = await _userManager.CreateAsync(User,Password);

            if (identityResult.Succeeded)
            {
                _userSubject.NotifyOberserver(User);
                ViewBag.message = "Kayıt İşlemi Tamamlandı";
            }
            else
            {
                ViewBag.message = identityResult.Errors.ToList().First().Description;
            }
            return View();
        }
    }
}
