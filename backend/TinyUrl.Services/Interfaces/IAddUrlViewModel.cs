using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyUrl.Services.Interfaces
{
    public interface IAddUrlViewModel
    {
        public string Url { get; }
        public string TinyUrl { get; }
    }
}
