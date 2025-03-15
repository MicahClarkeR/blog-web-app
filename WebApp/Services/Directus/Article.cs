using HtmlAgilityPack;
using System.Text.Json;
using WebApp.Helpers;

namespace WebApp.Services.Directus
{
    public class Article
    {
        public Guid? id { get; set; }
        public Guid? header { get; set; }
        public string? status { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public string? user_created { get; set; }
        public string[]? category { get; set; }
        public string[]? tags { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? date_updated { get; set; }
        public object? custom_css { get; set; } = null;
        public object? custom_js { get; set; } = null;
        public string? slug { get; set; } = null;
        public string? summary { get; set; } = null;
        public string? header_credit { get; set; } = null;
        public string? header_credit_link { get; set; } = null;

        public User CreatedUser { get; private set; }
        public string[]? CustomJSUrls { get; private set; } = null;
        public string[]? CustomCssUrls { get; private set; } = null;
        public string? SlugValue => (slug != null) ? slug : id.ToString();

        private HtmlDocument? _htmlDocument = null;

        public HtmlDocument GetHtml()
        {
            if (_htmlDocument != null)
                return _htmlDocument;

            _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(content);

            return _htmlDocument;
        }

        public string GetSummary()
        {
            if (summary != null)
                return summary;

            HtmlDocument html = GetHtml();
            HtmlNodeCollection nodes = html.DocumentNode.SelectNodes("p");
            HtmlNode node;
            string text = string.Empty;
            int i = 0;

            do
            {
                node = nodes[i];
                text = node.InnerText;
                i++;
            }
            while (text.Trim() == string.Empty);

            return text;
        }

        public async Task LoadFileDataAsync(DirectusService service)
        {
            CustomJSUrls = await GetCustomCssUrlsAsync(service);
            CustomCssUrls = await GetCustomJSUrlsAsync(service);
            CreatedUser = (await service.GetUserAsync(user_created)).Data;
        }

        public User GetAuthor()
        {
            return CreatedUser;
        }

        private async Task<string[]?> GetCustomCssUrlsAsync(DirectusService directus)
        {
            if (custom_css == null)
                return null;

            return await GetFieldValuesAsync(directus, "custom_css");
        }

        private async Task<string[]?> GetCustomJSUrlsAsync(DirectusService directus)
        {
            if (custom_js == null)
                return null;

            return await GetFieldValuesAsync(directus, "custom_js");
        }

        private async Task<string[]> GetFieldValuesAsync(DirectusService directus, string field)
        {
            List<string> urls = new List<string>();

            using JsonDocument doc = await directus.GetCollectionItemFieldAsync("articles", id.ToString(), $"{field}.*");
            JsonElement root = doc.RootElement;

            // Navigate JSON dynamically
            JsonElement customCssArray = root.GetProperty("data").GetProperty(field);

            foreach (JsonElement item in customCssArray.EnumerateArray())
            {
                string fileId = item.GetProperty("directus_files_id").GetString();
                urls.Add(Assets.GetUrl(fileId));
            }

            return urls.ToArray();
        }
    }
}
