using Microsoft.AspNetCore.Mvc;
using WebApp.CommandPattern.Commands;
using WebApp.CommandPattern.Models;

namespace WebApp.CommandPattern.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();            
        }
    }
}
