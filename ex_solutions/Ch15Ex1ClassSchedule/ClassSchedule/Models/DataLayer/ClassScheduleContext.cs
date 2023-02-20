using Microsoft.EntityFrameworkCore;

namespace ClassSchedule.Models
{
    public class ClassScheduleContext : DbContext
    {
        public ClassScheduleContext(DbContextOptions<ClassScheduleContext> options)
            : base(options)
        { }

        public DbSet<Day> Days { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DayConfig());
            modelBuilder.ApplyConfiguration(new TeacherConfig());
            modelBuilder.ApplyConfiguration(new ClassConfig());
        }

    }
}
