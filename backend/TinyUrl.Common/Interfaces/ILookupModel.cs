using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyUrl.Common.Interfaces
{
    public interface ILookupModel<T>
    {
        List<T> Records { get; }

        public int TotalRecords { get; }


    }
}
