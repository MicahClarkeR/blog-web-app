namespace WebApp.Services.Directus
{
    public class DataList<T>
    {
        public T[] data { get; set; } = Array.Empty<T>();
    }
}
