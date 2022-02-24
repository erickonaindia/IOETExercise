using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOETExercise.Models
{
    public class ScheduledDaysAmount
    {
        public TimeSpan InitTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double Amount { get; set; }
        public bool Weekend { get; set; }
    }
}
