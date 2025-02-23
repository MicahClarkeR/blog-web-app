using WebApp.Services.Directus;

namespace WebApp.Models
{
    public class HomeViewModel
    {
        public ArticleList Articles { get; set;} = new ArticleList();
    }
}
