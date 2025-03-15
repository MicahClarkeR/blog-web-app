using WebApp.Helpers;

namespace WebApp.Models
{
    public class Page
    {
        public string id { get; set; }
        public Guid? header_image { get; set; } = Guid.Empty;
        public string header { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
        public Guid? custom_css { get; set; } = null;
        public Guid? custom_js { get; set; } = null;

        public string? CustomCssUrl => (custom_css != null) ? $"/asset/get?id={custom_css}" : null;
        public string? CustomJsUrl => (custom_js != null) ? $"/asset/get?id={custom_js}" : null;
    }
}
