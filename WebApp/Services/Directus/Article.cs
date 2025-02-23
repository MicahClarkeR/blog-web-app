using HtmlAgilityPack;

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
    }
}
