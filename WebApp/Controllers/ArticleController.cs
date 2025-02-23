using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class ArticleController : Controller
    {
        const string ROUTE = "article";

        private readonly ILogger<ArticleController> _logger;
        private readonly DirectusService _directusService;

        public ArticleController(ILogger<ArticleController> logger, DirectusService directusService)
        {
            _logger = logger;
            _directusService = directusService;
        }

        [Route(ROUTE)]
        public async Task<IActionResult> Index(int page = 1, int count = 5)
        {
            var articles = await _directusService.GetArticlesAsync(count, (page - 1) * count);
            return View(new ArticleListModel
            {
                Articles = articles.data,
                Page = 1,
                Count = count
            });
        }

        [Route(ROUTE + "/{id}")]
        public async Task<IActionResult> Read(string id, string? backLink = null)
        {
            var article = await _directusService.GetArticleAsync(id);
            return View("Read", new ArticleReadModel
            {
                Article = article,
                BackLink = backLink
            });
        }
    }
}
