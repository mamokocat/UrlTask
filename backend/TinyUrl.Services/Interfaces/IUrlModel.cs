namespace TinyUrl.Common.Interfaces
{
    public interface IUrlModel
    {
        public int Id { get; }
        public string RelativePath { get; }
        public string RedirectUrl { get; }
        public int VisitCount { get; }
        public DateTime CreatedDate { get; }
    }
}
