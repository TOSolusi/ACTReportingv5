using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.Models
{
    public class DisplayRecordModel
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public int Week { get; set; }
       // public string Day {  get; set; }
        public string DateIn { get; set; }
        public string TimeIn { get; set; }
        public string DateOut { get; set; }
        public string TimeOut { get; set; }
        public string TotalHours { get; set; }
        //public TimeSpan Unaccounted { get; set; }
        public string? DailyTotal { get; set; }
        public string Remarks { get; set; }
    }
}
