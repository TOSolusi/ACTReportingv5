using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.ViewModels
{
    public class FileLocationViewModel
    {
        public string FileReportSettings { get; set; }
        public string FileSettings { get; set; }
        public FileLocationViewModel()
        {
            FileReportSettings = "Settings/ReportSettings.json";
            FileSettings = "Settings/ConnectionSettings.json";
        }
    }
}
