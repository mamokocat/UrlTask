using TinyUrl.Services.Interfaces;

namespace TinyUrl.API.Models
{
    public class AddUrlViewModel : IAddUrlViewModel
    {
        public string Url { get; set; }
        public string TinyUrl { get; set; }
    }
}
