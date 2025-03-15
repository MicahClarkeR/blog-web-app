namespace WebApp.Services.Directus
{
    public class Asset
    {
        public string? Id { get; set; } = null;
        public string? Storage { get; set; } = null;
        public string? Filename_Disk { get; set; } = null;
        public string? Filename_Download { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Type { get; set; } = null;
        public string? Folder { get; set; } = null;
        public DateTime? Created_On { get; set; } = null;
        public string? Uploaded_By { get; set; } = null;
        public DateTime? Uploaded_On { get; set; } = null;
        public string? Modified_By { get; set; } = null;
        public DateTime? Modified_On { get; set; } = null;
        public int? Filesize { get; set; } = null;
        public int? Width { get; set; } = null;
        public int? Height { get; set; } = null;
        public double? Focal_Point_X { get; set; } = null;
        public double? Focal_Point_Y { get; set; } = null;
        public double? Duration { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? Location { get; set; } = null;
        public List<string> Tags { get; set; } = new List<string>();
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}
