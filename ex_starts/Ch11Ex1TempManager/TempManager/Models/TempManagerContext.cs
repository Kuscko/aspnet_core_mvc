using Microsoft.EntityFrameworkCore;

namespace TempManager.Models
{
    public class TempManagerContext : DbContext
    {
        public TempManagerContext(DbContextOptions<TempManagerContext> options)
            : base(options)
        { }

        public DbSet<Temp> Temps { get; set; }
    }
}
