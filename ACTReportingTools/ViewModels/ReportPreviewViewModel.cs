using ACTReportingTools.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.ViewModels
{
    public class ReportPreviewViewModel : Screen
    {
        public ObservableCollection<RecordModel> _results { get; set; }
        public ObservableCollection<DisplayRecordModel> displayResult { get; set; }
        public ReportPreviewViewModel(ObservableCollection<RecordModel> records)
        {
            displayResult = new();
            
            foreach(var record in records)
            {
                DisplayRecordModel displayRecord = new();
                displayRecord.Name = record.Name;
                displayRecord.Group = record.Group;

                CultureInfo cul = CultureInfo.CurrentCulture;
                displayRecord.Week = cul.Calendar.GetWeekOfYear(record.TimeIn, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                displayRecord.DateIn = DateOnly.FromDateTime(record.TimeIn).ToString("dd-MM-yyyy") +" " + record.TimeIn.DayOfWeek.ToString(); 
                displayRecord.TimeIn = TimeOnly.FromDateTime(record.TimeIn).ToString();
                displayRecord.DateOut = DateOnly.FromDateTime(record.TimeOut).ToString("dd-MM-yyyy") + " " + record.TimeOut.DayOfWeek.ToString();
                displayRecord.TimeOut = TimeOnly.FromDateTime(record.TimeOut).ToString();
                displayRecord.TotalHours  = record.TotalHours.ToString(@"dd\:hh\:mm\:ss");
                //var total = Double.Abs(record.TotalHours.TotalMinutes);
                //displayRecord.TotalHours = $"{ Double.Abs(total/60)}:{(total % 60)}";
                displayRecord.Remarks = record.Remarks;
                displayResult.Add(displayRecord);
                         
            }


        }
    }
}
