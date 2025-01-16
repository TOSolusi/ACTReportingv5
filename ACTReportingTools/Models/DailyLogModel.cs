using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.Models
{
    public class DailyLogModel
    {
        public DateTime DateRegistered { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public TimeSpan? TotalTime { get; set; }
        //public TimeSpan?  TotalHours { get; set; }
        public string status { get; set; }

        public void AddDate(DateTime timeBadge)
        {
            if (TimeIn == null)
            { TimeIn = timeBadge.TimeOfDay; }
            else
            {
                TimeOut = timeBadge.TimeOfDay;
            }

        }
    }
}
