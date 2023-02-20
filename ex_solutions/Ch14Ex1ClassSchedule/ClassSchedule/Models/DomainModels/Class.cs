using System.ComponentModel.DataAnnotations;

namespace ClassSchedule.Models
{
    public class Class
    {
        public int ClassId { get; set; }                    // PK

        [StringLength(200, ErrorMessage = "Title may not exceed 200 characters.")]
        [Required(ErrorMessage = "Please enter a class title.")]
        public string Title { get; set; }

        [Range(100, 500, ErrorMessage = "Class number must be between 100 and 500.")]
        [Required(ErrorMessage = "Please enter a class number.")]
        public int? Number { get; set; }

        [Display(Name = "Time")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter numbers only for class time.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Class time must be 4 characters long.")]
        [Required(ErrorMessage = "Please enter a class time (in military time format).")]
        public string MilitaryTime { get; set; }

        public int TeacherId { get; set; }                  // Foreign key property
        public Teacher Teacher { get; set; }                // Navigation property

        public int DayId { get; set; }                      // Foreign key property
        public Day Day { get; set; }                        // Navigation property
    }
}
