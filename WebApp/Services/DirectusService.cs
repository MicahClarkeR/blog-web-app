using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Text.Json;
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
            _client.BaseAddress = new Uri("https://blog-directus-ot86s.ondigitalocean.app/");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer fB0c3CaNvVWhicR-kgOgFYMAPpooUUj7");
        }

        public async Task<ArticleList> GetArticlesAsync(int limit = -1, int offset = 0, Status status = Status.published)
        {
            var data = await _client.GetFromJsonAsync<ArticleList>($"items/articles?limit={limit}&offset={offset}&filter[_and][1][status][_eq]={status.ToString()}");

            foreach (var article in data.data)
                await article.LoadFileDataAsync(this);
            
            return data ?? new ArticleList();
        }

        public async Task<Article?> GetArticleAsync(string id)
        {
            var data = await _client.GetFromJsonAsync<SingleData<Article>>($"items/articles/{id}");
            await data?.Data.LoadFileDataAsync(this);
            return data?.Data;
        }

        public async Task<Article?> GetArticleBySlugAsync(string slug)
        {
            var data = await _client.GetFromJsonAsync<ArticleList>($"items/articles?filter[_and][1][slug][_eq]={slug}");
            await data?.data[0].LoadFileDataAsync(this);
            return data?.data[0];
        }

        public async Task<Page?> GetPageAsync(string id)
        {
            var data = await _client.GetFromJsonAsync<SingleData<Page>>($"items/pages/{id}");
            return data?.Data;
        }

        public async Task<Asset?> GetFileAssetAsync(string id)
        {
            var data = await _client.GetFromJsonAsync<SingleData<Asset>>($"files/{id}");
            return data?.Data;
        }

        public async Task<string> GetFileTextAsync(string id)
        {
            var data = await _client.GetStringAsync($"assets/{id}");
            return data;
        }

        public async Task<byte[]> GetFileContentAsync(string id, IQueryCollection? queryParameters = null)
        {
            string queryString = (queryParameters != null) ?
                string.Join('&', queryParameters?.Select(v => $"{v.Key}={v.Value}")) :
                string.Empty;

            var data = await _client.GetByteArrayAsync($"assets/{id}?download&{queryString}");
            return data;
        }

        public async Task<JsonDocument> GetCollectionItemFieldAsync(string collection, string id, string query)
        {
            var data = await _client.GetStringAsync($"items/{collection}/{id}?fields={query}");
            JsonDocument doc = JsonDocument.Parse(data);

            return doc;
        }

        public async Task<SingleData<User>> GetUserAsync(string id)
        {
            var data = await _client.GetFromJsonAsync<SingleData<User>>($"users/{id}");
            return data;
        }

        public async Task<Subscriber?> GetSubscriberByEmailAsync(string email)
        {
            string encryptedEmail = AESHelper.Encrypt(email);
            DataList<Subscriber> users = await _client.GetFromJsonAsync<DataList<Subscriber>>($"items/subscribers?filter[_and][1][email][_eq]={encryptedEmail}") ?? new DataList<Subscriber>();

            return (users.data.Length > 0) ? users.data[0] : null;
        }

        public enum Status
        {
            draft,
            published, 
            archive
        }
    }
}
