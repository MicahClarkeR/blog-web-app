using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DirectusService _directusService;

        public HomeController(ILogger<HomeController> logger, DirectusService directusService)
        {
            _logger = logger;
            _directusService = directusService;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _directusService.GetArticlesAsync(limit: 3);
            return View(new HomeViewModel
            {
                Articles = articles
            });
        }

        public IActionResult Privacy()
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
