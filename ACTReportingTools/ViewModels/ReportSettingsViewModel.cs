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

        public JObject SettingsConfig { get; set; }

        public ObservableCollection<string> HourNumbers { get; set; }
        public ObservableCollection<string> MinuteNumbers { get; set; }
        public ObservableCollection<string> DurationNumbers { get; set; }
        public ObservableCollection<string> DurationDaily { get; set; }
        public ObservableCollection<string> DurationWeek { get; set; }


        public TimeOnly TimeTimeInFrom { get; set; }
        public TimeOnly TimeTimeInTo { get; set; }
        public TimeOnly TimeTimeOutFrom { get; set; }
        public TimeOnly TimeTimeOutTo { get; set; }
        public TimeOnly TimeBreakTimeFrom { get; set; }
        public TimeOnly TimeBreakTimeTo { get; set; }
        public TimeOnly TimeBreakTimeFriFrom { get; set; }
        public TimeOnly TimeBreakTimeFriTo { get; set; }
        public string TimeOutFrom { get; set; }
        public string TimeOutTo { get; set; }
        public string BreakTimeFrom { get; set; }
        public string BreakTimeTo { get; set; }
        public string BreakTimeDuration { get; set; }
        public string BreakTimeFriFrom { get; set; }
        public string BreakTimeFriTo { get; set; }
        public string BreakTimeFriDuration { get; set; }
        public string GracePeriod { get; set; }
        public string DwellTime { get; set; }
        public string  dailyTotal { get; set; }
        public string weeklyTotal { get; set; }

        public string FileReportSettings { get; set; }
        
        public ReportSettingsViewModel()
        {
            //TimeInFromHour = "0";
            //TimeInFromMinutes = "0";
            //TimeInToHour = "0";
            //TimeInToMinutes = "0";

            menuViewModel = IoC.Get<MenuViewModel>();
            //string SettingsFileName = "Settings/ReportSettings.json"; 

            FileReportSettings = IoC.Get<FileLocationViewModel>().FileReportSettings;

            HourNumbers = ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"];
            MinuteNumbers = ["00", "15", "30", "45"];
            DurationNumbers = ["00", "15", "30", "45", "60", "75", "90", "105", "120"];
            DurationDaily = ["05", "06", "07", "08", "09", "10"];
            DurationWeek = ["30", "35", "40", "45", "50", "55", "60"];

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
            dailyTotal= (string)SettingsConfig["WorkDuration"];
            weeklyTotal = (string)SettingsConfig["TotalWeeklyHours"];

            TimeInFromHour = TimeInFrom.Substring(0, 2);
            TimeInFromMinutes = TimeInFrom[^2..];

            TimeInToHour = TimeInTo.Substring(0, 2);
            TimeInToMinutes = TimeInTo[^2..];

            TimeOutFromHour = TimeOutFrom.Substring(0, 2);
            TimeOutFromMinutes = TimeOutFrom[^2..];

            TimeOutToHour = TimeOutTo.Substring(0, 2);
            TimeOutToMinutes = TimeOutTo[^2..];

            BreakTimeFromHour = BreakTimeFrom.Substring(0, 2);
            BreakTimeFromMinutes = BreakTimeFrom[^2..];

            BreakTimeToHour = BreakTimeTo.Substring(0, 2);
            BreakTimeToMinutes = BreakTimeTo[^2..];

            BreakTimeFriFromHour = BreakTimeFriFrom.Substring(0, 2);
            BreakTimeFriFromMinutes = BreakTimeFriFrom[^2..];

            BreakTimeFriToHour = BreakTimeFriTo.Substring(0, 2);
            BreakTimeFriToMinutes = BreakTimeFriTo[^2..];

        }

        private string breakTimeFriToHour;

        public string BreakTimeFriToHour
        {
            get { return breakTimeFriToHour; }
            set
            {
                breakTimeFriToHour = value;
                NotifyOfPropertyChange(() => BreakTimeFriToHour);
                if (BreakTimeFriToMinutes == null)
                {
                    BreakTimeFriToMinutes = "0";
                }
                TimeBreakTimeFriTo = new TimeOnly(int.Parse(BreakTimeFriToHour), int.Parse(BreakTimeFriToMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }

        private string breakTimeFriToMinutes;

        public string BreakTimeFriToMinutes
        {
            get { return breakTimeFriToMinutes; }
            set
            {
                breakTimeFriToMinutes = value;
                NotifyOfPropertyChange(() => BreakTimeFriToMinutes);
                TimeBreakTimeFriTo = new TimeOnly(int.Parse(BreakTimeFriToHour), int.Parse(BreakTimeFriToMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }


        private string breaktTimeFriFromHour;

        public string BreakTimeFriFromHour
        {
            get { return breaktTimeFriFromHour; }
            set
            {
                breaktTimeFriFromHour = value;
                NotifyOfPropertyChange(() => BreakTimeFriFromHour);
                if (BreakTimeFriFromMinutes == null)
                {
                    BreakTimeFriFromMinutes = "0";
                }
                TimeBreakTimeFriFrom = new TimeOnly(int.Parse(BreakTimeFriFromHour), int.Parse(BreakTimeFriFromMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }

        private string breakTimeFriFromMinutes;

        public string BreakTimeFriFromMinutes
        {
            get { return breakTimeFriFromMinutes; }
            set
            {
                breakTimeFriFromMinutes = value;
                TimeBreakTimeFriFrom = new TimeOnly(int.Parse(BreakTimeFriFromHour), int.Parse(BreakTimeFriFromMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }


        private string breakTimeToHour;

        public string BreakTimeToHour
        {
            get { return breakTimeToHour; }
            set 
            { 
                breakTimeToHour = value;
                NotifyOfPropertyChange(() => BreakTimeToHour);
                if (BreakTimeToMinutes == null)
                {
                    BreakTimeToMinutes = "0";
                }
                TimeBreakTimeTo = new TimeOnly(int.Parse(BreakTimeToHour), int.Parse(BreakTimeToMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }

        private string breakTimeToMinutes;

        public string BreakTimeToMinutes
        {
            get { return breakTimeToMinutes; }
            set { 
                breakTimeToMinutes = value;
                NotifyOfPropertyChange(() => BreakTimeToMinutes);
                TimeBreakTimeTo = new TimeOnly(int.Parse(BreakTimeToHour), int.Parse(BreakTimeToMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }


        private string breaktTimeFromHour;

        public string BreakTimeFromHour
        {
            get { return breaktTimeFromHour; }
            set 
            { 
                breaktTimeFromHour = value;
                NotifyOfPropertyChange(() => BreakTimeFromHour);
                if (BreakTimeFromMinutes == null)
                {
                    BreakTimeFromMinutes = "0";
                }
                TimeBreakTimeFrom = new TimeOnly(int.Parse(BreakTimeFromHour), int.Parse(BreakTimeFromMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }

        private string breakTimeFromMinutes;

        public string BreakTimeFromMinutes
        {
            get { return breakTimeFromMinutes; }
            set 
            { 
                breakTimeFromMinutes = value;
                TimeBreakTimeFrom = new TimeOnly(int.Parse(BreakTimeFromHour), int.Parse(BreakTimeFromMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }


        private bool checkSunday;

        public bool CheckSunday
        {
            get { return checkSunday; }
            set 
            { 
                checkSunday = value;
                NotifyOfPropertyChange(() => CheckSunday);
            }
        }


        private bool checkSaturday;

        public bool CheckSaturday
        {
            get { return checkSaturday; }
            set 
            { 
                checkSaturday = value;
                NotifyOfPropertyChange(() => CheckSaturday);
            }
        }


        private string timeOutToHour;

        public string TimeOutToHour
        {
            get { return timeOutToHour; }
            set 
            { 
                timeOutToHour = value;
                NotifyOfPropertyChange(() => TimeOutToHour);
                if (TimeOutToMinutes == null)
                {
                    TimeOutToMinutes = "0";
                }
                TimeTimeOutTo = new TimeOnly(int.Parse(TimeOutToHour), int.Parse(TimeOutToMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }

        private string timeOutToMinutes;

        public string TimeOutToMinutes
        {
            get { return timeOutToMinutes; }
            set 
            { 
                timeOutToMinutes = value;
                NotifyOfPropertyChange(() => TimeOutToMinutes);
                TimeTimeOutTo = new TimeOnly(int.Parse(TimeOutToHour), int.Parse(TimeOutToMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }


        private string timeOutFromHour;

        public string TimeOutFromHour
        {
            get { return timeOutFromHour; }
            set 
            { 
                timeOutFromHour = value;
                NotifyOfPropertyChange(() => TimeOutFromHour);
                if (TimeOutFromMinutes == null)
                {
                    TimeOutFromMinutes = "0";
                }
                TimeTimeOutFrom = new TimeOnly(int.Parse(TimeOutFromHour), int.Parse(TimeOutFromMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }

        private string timeOutFromMinutes;

        public string TimeOutFromMinutes
        {
            get { return timeOutFromMinutes; }
            set 
            { 
                timeOutFromMinutes = value; 
                NotifyOfPropertyChange(() => TimeOutFromMinutes);
                TimeTimeOutFrom = new TimeOnly(int.Parse(TimeOutFromHour), int.Parse(TimeOutFromMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }


        private string timeInFromHour;

        public string TimeInFromHour
        {
            get { return timeInFromHour; }
            set 
            { 
                timeInFromHour = value;
                NotifyOfPropertyChange(() => TimeInFromHour);
                if (TimeInFromMinutes == null)
                {
                    TimeInFromMinutes = "0";
                }
                TimeTimeInFrom = new TimeOnly(int.Parse(TimeInFromHour), int.Parse(TimeInFromMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }

        private string timeInFromMinutes;

        public string TimeInFromMinutes
        {
            get { return timeInFromMinutes; }
            set 
            { 
                timeInFromMinutes = value;
                NotifyOfPropertyChange(() => TimeInFromMinutes);
                TimeTimeInFrom = new TimeOnly(int.Parse(TimeInFromHour), int.Parse(TimeInFromMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }

        private string timeInToHour;

        public string TimeInToHour
        {
            get { return timeInToHour; }
            set 
            { 
                timeInToHour = value; 
                NotifyOfPropertyChange(() => TimeInToHour);
                if (TimeInToMinutes == null)
                {
                    TimeInToMinutes = "0";
                }
                TimeTimeInTo = new TimeOnly(int.Parse(TimeInToHour), int.Parse(TimeInToMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
        }
        

        private string timeInToMinutes  ;

        public string TimeInToMinutes
        {
            get { return timeInToMinutes; }
            set
            {
                timeInToMinutes = value;
                NotifyOfPropertyChange(() => TimeInToMinutes);
                TimeTimeInTo = new TimeOnly(int.Parse(TimeInToHour), int.Parse(TimeInToMinutes));
                NotifyOfPropertyChange(() => CanButtonSaveSettings);
            }
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

        public void ButtonSaveSettings()
        {
            SettingsConfig["TimeInFrom"] = TimeInFromHour + TimeInFromMinutes;
            SettingsConfig["TimeInTo"] = TimeInToHour + TimeInToMinutes;
            SettingsConfig["TimeOutFrom"] = TimeOutFromHour + TimeOutFromMinutes;
            SettingsConfig["TimeOutTo"] = TimeOutToHour + TimeOutToMinutes;
            SettingsConfig["CheckSaturday"] = CheckSaturday.ToString();
            SettingsConfig["CheckSunday"] = CheckSunday.ToString();
            SettingsConfig["BreakTimeFrom"] = BreakTimeFromHour + breakTimeFromMinutes;
            SettingsConfig["BreakTimeTo"] = BreakTimeToHour + BreakTimeToMinutes;
            SettingsConfig["BreakTimeDuration"] = BreakTimeDuration;
            SettingsConfig["BreakTimeFriFrom"] = BreakTimeFriFromHour + breakTimeFriFromMinutes;
            SettingsConfig["BreakTimeFriTo"] = BreakTimeFriToHour + BreakTimeFriToMinutes;
            SettingsConfig["BreakTimeFriDuration"] = BreakTimeFriDuration;
            SettingsConfig["GracePeriod"] = GracePeriod;
            SettingsConfig["DwellTime"] = DwellTime;
            SettingsConfig["WorkDuration"] = dailyTotal;
            SettingsConfig["TotalWeeklyHours"] = weeklyTotal;

            ConfigHelper.SetConfig(SettingsConfig, FileReportSettings);
            MessageBox.Show("Report Settings Saved");
            menuViewModel.ButtonReportDate();
        }
       
        public void ButtonCancel()
        {
            menuViewModel.ButtonReportDate();
        }

        public bool CanButtonSaveSettings
        {
            get
            {
                bool value = false;
                if (TimeTimeInFrom > TimeTimeInTo) //
                {
                    value = false;
                    ErrorMessage = "Time In From is Larger than Time In To.";
                    VisibleError = Visibility.Visible;
                }
                else if (TimeTimeOutFrom > TimeTimeOutTo)
                {
                    value = false;
                    ErrorMessage = ErrorMessage + "Time Out From is Larger than Time Out To.";
                    VisibleError = Visibility.Visible;
                }
                else if (TimeTimeInTo > TimeTimeOutFrom)
                {
                    value = false;
                    ErrorMessage = ErrorMessage + "Time In To is Larger than Time Out From.";
                    VisibleError = Visibility.Visible;
                }
                else if (TimeBreakTimeFrom > TimeBreakTimeTo)
                {
                    value = false;
                    ErrorMessage = ErrorMessage + "Break Time Start is Later than End of Break Time.";
                    VisibleError = Visibility.Visible;
                }
                else if (TimeBreakTimeFriFrom > TimeBreakTimeFriTo)
                {
                    value = false;
                    ErrorMessage = ErrorMessage + "Break Time on Friday Start is Later than End of Break Time on Friday.";
                    VisibleError = Visibility.Visible;
                }
                else
                {
                    value = true;
                    ErrorMessage = "";
                    VisibleError = Visibility.Collapsed;
                }
                //VisibleError = Visibility.Visible;
                return value;
            }
        }

        private Visibility visibleError;

        public Visibility VisibleError
        {
            get { return visibleError; }
            set 
            { 
                visibleError = value;
                NotifyOfPropertyChange(() => VisibleError);
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set 
            { 
                errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }



    }
}
