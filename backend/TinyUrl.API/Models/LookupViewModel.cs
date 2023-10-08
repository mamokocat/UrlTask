namespace TinyUrl.API.Models
{
    public class LookupViewModel<T>
    {
        public List<T> Records { get; set; } = new List<T>();
        public int TotalRecords { get; set; }
    }
}
