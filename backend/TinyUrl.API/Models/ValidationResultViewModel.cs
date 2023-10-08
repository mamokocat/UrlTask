namespace TinyUrl.API.Models
{
    public class ValidationResultViewModel
    {
        public bool IsValid { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}
