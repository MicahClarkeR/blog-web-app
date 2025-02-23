using WebApp.Services.Directus;

namespace WebApp.Models
{
    public class ArticleReadModel
    {
        public Article? Article { get; set; } = null;
        public string? BackLink { get; set; } = null;
    }
}
