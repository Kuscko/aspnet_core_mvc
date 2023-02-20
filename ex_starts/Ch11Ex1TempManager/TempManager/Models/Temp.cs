using System;

namespace TempManager.Models
{
    public class Temp
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public double? Low { get; set; }
        public double? High { get; set; }
    }
}
