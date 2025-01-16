using ACTReportingTools.Models;
using ACTReportingTools.ViewModels;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.Helpers
{
    public class ProcessReport
    {
        public string _startDate { get; set; }
        public string _endDate { get; set; }
        public JObject SettingsConfig { get; set; }
        public string TimeInFrom { get; set; }
        public string TimeInTo { get; set; }
        public string TimeOutTo { get; set; }
        public string TimeOutFrom { get; set; }
        public bool CheckSaturday { get; set; }
        public bool CheckSunday { get; set; }
        public string BreakTimeFrom { get; set; }
        public string BreakTimeTo { get; set; }
        public string BreakTimeDuration { get; set; }
        public string BreakTimeFriFrom { get; set; }
        public string BreakTimeFriTo { get; set; }
        public string BreakTimeFriDuration { get; set; }
        public string GracePeriod { get; set; }
        public string DwellTime { get; set; }

        public string FileReportSettings { get; set; }
        public ProcessReport(string startDate, string endDate)
        {
            _startDate = startDate;
            _endDate = endDate;

            FileReportSettings = IoC.Get<FileLocationViewModel>().FileReportSettings;
            SettingsConfig = ConfigHelper.LoadConfig(FileReportSettings);
            TimeInFrom = (string)SettingsConfig["TimeInFrom"];
            TimeInTo = (string)SettingsConfig["TimeInTo"];
            TimeOutFrom = (string)SettingsConfig["TimeOutFrom"];
            TimeOutTo = (string)SettingsConfig["TimeOutTo"];
            CheckSaturday = (bool)SettingsConfig["CheckSaturday"];
            CheckSunday = (bool)SettingsConfig["CheckSunday"];
            BreakTimeFrom = (string)SettingsConfig["BreakTimeFrom"];
            BreakTimeTo = (string)SettingsConfig["BreakTimeTo"];
            BreakTimeDuration = (string)SettingsConfig["BreakTimeDuration"];
            BreakTimeFriFrom = (string)SettingsConfig["BreakTimeFriFrom"];
            BreakTimeFriTo = (string)SettingsConfig["BreakTimeFriTo"];
            BreakTimeFriDuration = (string)SettingsConfig["BreakTimeFriDuration"];
            GracePeriod = (string)SettingsConfig["GracePeriod"];
            DwellTime = (string)SettingsConfig["DwellTime"];

            var result = Samplerun();

        }

        private ObservableCollection<EventLogModel> Samplerun()
        {
            SampleModel test = new();
            return test.AddSample();
            
        }

    }
}
