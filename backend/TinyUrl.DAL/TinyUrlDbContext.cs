using Microsoft.EntityFrameworkCore;
using TinyUrl.DAL.Models;

namespace TinyUrl.DAL
{
    public class TinyUrlDbContext : DbContext
    {
        private string connectionString { get; set; }

        public TinyUrlDbContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        public TinyUrlDbContext(DbContextOptions<TinyUrlDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(this.connectionString,
                new MySqlServerVersion(new Version(11, 1, 2)));
        }

        public virtual DbSet<UrlDbModel> Urls { get; set; }
    }
}
