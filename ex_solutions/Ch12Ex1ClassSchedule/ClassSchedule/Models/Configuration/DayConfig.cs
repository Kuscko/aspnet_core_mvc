using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassSchedule.Models
{
    internal class DayConfig : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> entity)
        {
            entity.HasData(
               new Day { DayId = 1, Name = "Monday" },
               new Day { DayId = 2, Name = "Tuesday" }, 
               new Day { DayId = 3, Name = "Wednesday" },
               new Day { DayId = 4, Name = "Thursday" },
               new Day { DayId = 5, Name = "Friday" },
               new Day { DayId = 6, Name = "Saturday" },
               new Day { DayId = 7, Name = "Sunday" }
            );
        }
    }

}
