using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyUrl.Common.Interfaces;

namespace TinyUrl.Services.Models
{
    public class UrlModel : IUrlModel
    {
        public int Id { get; set; }
        public string RelativePath { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
        public int VisitCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
