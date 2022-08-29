using IkapatigiCapstone.Models;
using Microsoft.EntityFrameworkCore;


namespace IkapatigiCapstone.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tag> Tag { get; set; }

    }
}


