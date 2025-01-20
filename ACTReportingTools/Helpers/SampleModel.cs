using ACTReportingTools.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.Helpers
{
    public class SampleModel
    {
        public ObservableCollection<EventLogModel> events { get; set; }
        public SampleModel()
        {
           

            
          
        }

        public ObservableCollection<EventLogModel> AddSample()
        {
            events = new ObservableCollection<EventLogModel>();
            events.Add(new EventLogModel() { EventID = 1, When = DateTime.Parse("10/01/2025 07:45:00"), Door = 1, EventData= 324, OriginalForename = "Test", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 2, When = DateTime.Parse("10/01/2025 10:00:00"), Door = 2, EventData = 324, OriginalForename = "Test", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 3, When = DateTime.Parse("10/01/2025 10:12:00"), Door = 1, EventData = 324, OriginalForename = "Test", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 4, When = DateTime.Parse("10/01/2025 12:30:00"), Door = 2, EventData = 324, OriginalForename = "Test", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 5, When = DateTime.Parse("10/01/2025 13:17:00"), Door = 1, EventData = 324, OriginalForename = "Test", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 6, When = DateTime.Parse("10/01/2025 16:45:00"), Door = 2, EventData = 324, OriginalForename = "Test", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 7, When = DateTime.Parse("10/01/2025 16:50:00"), Door = 1, EventData = 324, OriginalForename = "Test", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 8, When = DateTime.Parse("10/01/2025 17:15:00"), Door = 2, EventData = 324, OriginalForename = "Test", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 9, When = DateTime.Parse("10/01/2025 18:00:00"), Door = 2, EventData = 324, OriginalForename = "Test", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 10, When = DateTime.Parse("10/01/2025 19:00:00"), Door = 2, EventData = 324, OriginalForename = "Test", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 10, When = DateTime.Parse("10/01/2025 22:00:00"), Door = 1, EventData = 324, OriginalForename = "Test4", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 10, When = DateTime.Parse("11/01/2025 10:00:00"), Door = 2, EventData = 321, OriginalForename = "Test4", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 10, When = DateTime.Parse("11/01/2025 11:00:00"), Door = 1, EventData = 321, OriginalForename = "Test4", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 10, When = DateTime.Parse("11/01/2025 19:00:00"), Door = 2, EventData = 321, OriginalForename = "Test4", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 1, When = DateTime.Parse("10/01/2025 07:30:00"), Door = 1, EventData = 32, OriginalForename = "Test2", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 2, When = DateTime.Parse("10/01/2025 10:00:00"), Door = 2, EventData = 32, OriginalForename = "Test2", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 3, When = DateTime.Parse("10/01/2025 10:12:00"), Door = 1, EventData = 32, OriginalForename = "Test2", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 4, When = DateTime.Parse("10/01/2025 12:30:00"), Door = 2, EventData = 32, OriginalForename = "Test2", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 5, When = DateTime.Parse("10/01/2025 13:17:00"), Door = 1, EventData = 32, OriginalForename = "Test2", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 6, When = DateTime.Parse("10/01/2025 16:45:00"), Door = 2, EventData = 32, OriginalForename = "Test2", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 7, When = DateTime.Parse("10/01/2025 16:50:00"), Door = 1, EventData = 32, OriginalForename = "Test2", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 8, When = DateTime.Parse("10/01/2025 17:05:00"), Door = 2, EventData = 32, OriginalForename = "Test2", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 9, When = DateTime.Parse("10/01/2025 18:00:00"), Door = 2, EventData = 32, OriginalForename = "Test2", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 10, When = DateTime.Parse("10/01/2025 19:00:00"), Door = 2, EventData = 32, OriginalForename = "Test2", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 10, When = DateTime.Parse("11/01/2025 9:00:00"), Door = 1, EventData = 31, OriginalForename = "Test3", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 10, When = DateTime.Parse("11/01/2025 10:00:00"), Door = 2, EventData = 31, OriginalForename = "Test3", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 10, When = DateTime.Parse("11/01/2025 11:00:00"), Door = 1, EventData = 31, OriginalForename = "Test3", OriginalSurname = "Case" });
            events.Add(new EventLogModel() { EventID = 10, When = DateTime.Parse("11/01/2025 19:00:00"), Door = 2, EventData = 31, OriginalForename = "Test3", OriginalSurname = "Case" });

            return events;
        }

        //public long EventID { get; set; }
        //public DateTime When { get; set; }
        //public long TimeStamp { get; set; }
        //public int Event { get; set; }
        //public int Controller { get; set; }
        //public int Door { get; set; }
        //public long EventData { get; set; }
        //public string OriginalForename { get; set; }
        //public string OriginalSurname { get; set; }
    }
}
