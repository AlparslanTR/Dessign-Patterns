using Base.Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.TemplatePattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Customer> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<Customer> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task <IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
