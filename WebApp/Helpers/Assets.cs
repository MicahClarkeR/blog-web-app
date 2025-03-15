using WebApp.Properties;

namespace WebApp.Helpers
{
    public static class Assets
    {
        public static string GetImage(Guid? id, int width = -1, int height = -1, int quality = 80, string format = "jpg", string fit = "contain")
        {
            string link = GetUrl(id?.ToString() + "&bytes=1");

            if (width != -1)
                link += $"&width={width}";

            if (height != -1)
                link += $"&height={height}";

            if (quality != -1)
                link += $"&quality={quality}";

            if (!string.IsNullOrEmpty(format))
                link += $"&format={format}";

            if (!string.IsNullOrEmpty(format))
                link += $"&fit={fit}";

            return link;
        }

        public static string GetUrl(string? id)
            => $"/asset/get?id={id}";
    }
}
