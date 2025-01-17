using ACTReportingTools.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.ViewModels
{
    public class ReportPreviewViewModel : Screen
    {
        public ObservableCollection<RecordModel> results { get; set; }
        public ReportPreviewViewModel(ObservableCollection<RecordModel> records)
        {
                results = records;
        }
    }
}
