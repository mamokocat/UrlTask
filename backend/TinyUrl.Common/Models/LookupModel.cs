using TinyUrl.Common.Interfaces;

namespace TinyUrl.Common.Models
{
    public class LookupModel<T> : ILookupModel<T>
    {
        public List<T> Records { get; set; } = new List<T>();

        public int TotalRecords { get; set; }
    }
}
