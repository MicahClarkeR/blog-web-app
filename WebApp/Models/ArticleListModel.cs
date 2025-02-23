using WebApp.Services.Directus;

namespace WebApp.Models
{
    public class ArticleListModel
    {
        public Article[] Articles { get; set; } = [];
        public int Page { get; set; } = 1;
        public int Count { get; set; } = 20;
        public int Found => Articles.Length;
    }
}
