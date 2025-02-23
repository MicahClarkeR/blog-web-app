using WebApp.Services.Directus;

namespace WebApp.Models.Partial
{
    public class PartialArticleListModel
    {
        public int Page { get; set; } = 1;
        public Article[] Articles { get; set; } = [];
        public string? BackLink { get; set; } = null;
    }
}
