using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class PageController : Controller
    {
        public const string ROUTE = "page";

        private readonly ILogger<PageController> _logger;
        private readonly DirectusService _directus;

        public PageController(ILogger<PageController> logger, DirectusService directusService)
        {
            _logger = logger;
            _directus = directusService;
        }

        [Route(ROUTE + "/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var page = await _directus.GetPageAsync(id);

            return View(new PageViewModel()
            {
                Page = page ?? new Page()
            });
        }
    }
}
