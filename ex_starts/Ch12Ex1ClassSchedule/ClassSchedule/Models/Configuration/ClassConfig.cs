using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassSchedule.Models
{
    internal class ClassConfig : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> entity)
        {
            entity.HasData(
               new Class { ClassId = 1, Title = "Sign Language", Number = 101, TeacherId = 1, DayId = 1, MilitaryTime = "1500" },
               new Class { ClassId = 2, Title = "Sign Language", Number = 301, TeacherId = 1, DayId = 2, MilitaryTime = "1100" },
               new Class { ClassId = 3, Title = "Logic", Number = 101, TeacherId = 4, DayId = 4, MilitaryTime = "1300" },
               new Class { ClassId = 4, Title = "Logic", Number = 201, TeacherId = 4, DayId = 4, MilitaryTime = "1500" },
               new Class { ClassId = 5, Title = "Early Childhood Education", Number = 101, TeacherId = 2, DayId = 3, MilitaryTime = "1000" },
               new Class { ClassId = 6, Title = "Early Childhood Education", Number = 401, TeacherId = 2, DayId = 5, MilitaryTime = "1000" },
               new Class { ClassId = 7, Title = "Calculus", Number = 101, TeacherId = 5, DayId = 1, MilitaryTime = "1300" },
               new Class { ClassId = 8, Title = "Calculus", Number = 201, TeacherId = 5, DayId = 3, MilitaryTime = "1300" },
               new Class { ClassId = 9, Title = "Nonviolence and Social Change", Number = 101, TeacherId = 3, DayId = 4, MilitaryTime = "1400" },
               new Class { ClassId = 10, Title = "Nonviolence and Social Change", Number = 201, TeacherId = 3, DayId = 5, MilitaryTime = "1400" }
            );
        }
    }

}
