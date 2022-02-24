using System;
using System.Collections.Generic;

namespace IOETExercise.Models
{
    public class DataObjectFiles
    {
        public string Name { get; set; }
        public List<Details> Details { get; set; } = new List<Details>();
    }

    public class Details
    {
        public WeekDays WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
