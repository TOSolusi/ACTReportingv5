using ACTReportingTools.Helpers;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ACTReportingTools.ViewModels
{
    public class ReportSettingsViewModel : Screen
    {
        public MenuViewModel menuViewModel { get; set; }

        public JObject _config { get; set; }

        public ObservableCollection<string> HourNumbers { get; set; }
        public ObservableCollection<string> MinuteNumbers { get; set; }
        public ObservableCollection<string> DurationNumbers { get; set; }
        public string TimeInHourFromString { get; set; }
        public string TimeInMinuteFromString { get; set; }
        public ReportSettingsViewModel()
        {
                menuViewModel = IoC.Get<MenuViewModel>();
            string SettingsFileName = "ReportSettings.json";

            HourNumbers = ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"];
            MinuteNumbers = ["00", "15", "30", "45"];
            DurationNumbers = ["00", "15", "30", "45", "60", "75", "90", "105", "120"];

            _config = ConfigHelper.LoadConfig(SettingsFileName);
            TimeInFrom = (string)_config["TimeInFrom"];
            TimeInTo = (string)_config["TimeInTo"];

            TimeInHourFromString = "08";
            TimeInMinuteFromString = "00";
            //TimeInFrom = "08:00";
            //TimeInTo = "10:00";
            //HourNumber = (string) _config["TimeInFrom"];
            //TimeInFrom = TimeOnly.Parse((string)_config["TimeInFrom"]);
            //TimeInTo = TimeOnly.Parse((string) _config["TimeInTo"]);  

        }



        private string timeInFrom;

        public string TimeInFrom
        {
            get { return timeInFrom; }
            set 
            { 
                timeInFrom = value;
                NotifyOfPropertyChange(() => TimeInFrom);
            }
        }

        private string timeInTo;

        public string TimeInTo
        {
            get { return timeInTo; }
            set
            {
                timeInTo = value;
                NotifyOfPropertyChange(() => TimeInTo);
            }
        }


       
        public void ButtonCancel()
        {
            menuViewModel.ButtonReportDate();
        }
    }
}
