using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.Models
{
    public class RecordWorkingModel
    {
        public string UserNumber { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        //public DateOnly DateIn { get; set; }
        //public DateOnly DateOut { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public DateTime TimeIn2 { get; set; }
        public TimeSpan TotalHours { get; set; }
        public TimeSpan UnaccountedHours { get; set; }
    }
}
