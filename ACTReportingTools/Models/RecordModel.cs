using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.Models
{
    public class RecordModel
    {
       
            public string CardNo { get; set; }
            public string Name { get; set; }
            public string Group { get; set; }
            public DateOnly Date { get; set; }
            public DateTime TimeIn { get; set; }
            public DateTime TimeOut { get; set; }
            public DateTime TotalHours { get; set; }
        
    }
}
