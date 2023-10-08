namespace TinyUrl.DAL.Models
{
    public class UrlDbModel
    {
        public int? Id { get; set; }
        public string RelativePath { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
        public int VisitCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
