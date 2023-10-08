using TinyUrl.Common.Interfaces;

namespace TinyUrl.API.Models
{
    public class AddEditViewModel: IAddEditViewModel
    {
        public string RelativePath { get; set; }
        public string RedirectUrl { get; set; }
    }
}
