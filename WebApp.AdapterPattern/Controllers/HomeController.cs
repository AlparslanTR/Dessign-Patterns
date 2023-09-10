using Microsoft.AspNetCore.Mvc;
using WebApp.AdapterPattern.Services;

namespace WebApp.AdapterPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageProcess _ımageProcess;
        private readonly ILogger _logger;

        public HomeController(IImageProcess ımageProcess, ILogger<HomeController> logger)
        {
            _ımageProcess = ımageProcess;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SaveImage()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> SaveImage(IFormFile formFile)
        {
            if (formFile is { Length:>0 })
            {
                var fileMemoryStream = new MemoryStream();
                await formFile.CopyToAsync(fileMemoryStream);
                _ımageProcess.AddWatermark(".Net Core Web App",formFile.FileName,fileMemoryStream);
            }
            return View();
        }
    }
}
