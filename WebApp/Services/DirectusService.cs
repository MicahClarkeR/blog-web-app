using System.Collections.Generic;
using WebApp.Models;
using WebApp.Properties;
using WebApp.Services.Directus;

namespace WebApp.Services
{
    public class DirectusService
    {
        private readonly HttpClient _client;

        public DirectusService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(Resources.Directus);
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Resources.Token}");
        }

        public async Task<ArticleList> GetArticlesAsync(int limit = -1, int offset = 0, Status status = Status.published)
        {
            var data = await _client.GetFromJsonAsync<ArticleList>($"items/articles?limit={limit}&offset={offset}&filter[_and][1][status][_eq]={status.ToString()}");
            return data ?? new ArticleList();
        }

        public async Task<Article?> GetArticleAsync(string id)
        {
            var data = await _client.GetFromJsonAsync<SingleData<Article>>($"items/articles/{id}");
            return data?.Data;
        }

        public async Task<Page?> GetPageAsync(string id)
        {
            var data = await _client.GetFromJsonAsync<SingleData<Page>>($"items/special_html/{id}");
            return data?.Data;
        }

        public enum Status
        {
            draft,
            published, 
            archive
        }
    }
}
