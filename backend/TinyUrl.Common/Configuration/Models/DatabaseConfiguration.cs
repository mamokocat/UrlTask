using TinyUrl.Common.Configuration.Interfaces;

namespace TinyUrl.Common.Configuration.Models
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string TinyUrlDb { get; set; }
    }
}
