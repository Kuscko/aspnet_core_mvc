using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassSchedule.Models
{
    public class Day
    {
        public int DayId { get; set; }                     // PK

        // no error messages included bc users won't be entering - this is so 
        // EF will generate a non-null nvarchar with length shorter than max
        [StringLength(10)]  
        [Required()]
        public string Name { get; set; }

        public ICollection<Class> Classes { get; set; }    // Navigation property
    }
}
