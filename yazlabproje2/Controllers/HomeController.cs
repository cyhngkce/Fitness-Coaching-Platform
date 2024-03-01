using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using yazlabproje2.Models;

namespace yazlabproje2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult UserPanel()
        {
            return View();
        }
        public IActionResult UpdateProfile()
        {
            return View();
        }
        public IActionResult TrainerLogin()
        {
            return View();
        }
        public IActionResult TrainerPanel()
        {
            return View();
        }

        public IActionResult UpdateTrainerProfile()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}