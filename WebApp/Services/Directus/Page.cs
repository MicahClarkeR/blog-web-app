namespace WebApp.Models
{
    public class Page
    {
        public string id { get; set; }
        public Guid? header_image { get; set; } = Guid.Empty;
        public string header { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
    }
}
