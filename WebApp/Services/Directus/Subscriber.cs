namespace WebApp.Services.Directus
{
    public class Subscriber
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Roundup_Frequency { get; set; }
        public string Roundup_Custom { get; set; }
        public string Latest_Issue { get; set; }
    }
}
