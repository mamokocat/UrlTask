namespace TinyUrl.API.Models
{
    public class UrlViewModel
    {
        public int Id { get; set; }
        public string RedirectUrl { get; set; }
        public string RelativePath { get; set; }
        public int VisitCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
