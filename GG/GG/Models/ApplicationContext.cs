using Microsoft.EntityFrameworkCore;

namespace GG.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
