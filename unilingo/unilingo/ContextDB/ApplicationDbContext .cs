using Microsoft.EntityFrameworkCore;
using unilingo.Model;
using unilingo.ContextDB;

namespace unilingo.ContextDB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VideoInformation> Videos { get; set; }
    }

}
