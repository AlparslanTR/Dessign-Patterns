using Microsoft.AspNetCore.Mvc;

namespace WebApp.ObserverPattern.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
