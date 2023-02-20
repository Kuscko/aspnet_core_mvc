using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassSchedule.Models
{
    internal class TeacherConfig : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> entity)
        {
            entity.HasData(
               new Teacher { TeacherId = 1, FirstName = "Anne", LastName = "Sullivan" },
               new Teacher { TeacherId = 2, FirstName = "Maria", LastName = "Montessori" },
               new Teacher { TeacherId = 3, FirstName = "Martin Luther", LastName = "King" },
               new Teacher { TeacherId = 4, FirstName = "", LastName = "Aristotle" },
               new Teacher { TeacherId = 5, FirstName = "Jaime", LastName = "Escalante" }
            );
        }
    }

}
