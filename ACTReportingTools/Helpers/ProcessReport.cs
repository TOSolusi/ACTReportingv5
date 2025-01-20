using ACTReportingTools.Models;
using ACTReportingTools.ViewModels;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Composition;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading;
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
        public string TimeInFriFrom { get; set; }
        public string TimeInFriTo { get; set; }
        public string TimeOutFriTo { get; set; }
        public string TimeOutFriFrom { get; set; }
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
        public TimeOnly timeInFrom { get; set; }
        public TimeOnly timeInTo { get; set; }
        public TimeOnly timeOutFrom { get; set; }
        public TimeOnly timeOutTo { get; set; }

        ObservableCollection<RecordModel> recordResult { get; set; }
        ObservableCollection<RecordModel> recordInCheck { get; set; }
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

            //for test run
            var result = Samplerun();

            recordInCheck = new();
            recordResult = new();

            int[] inputDoorNumber = { 1 };
            int[] outputDoorNumber = { 2 };

            foreach (EventLogModel a in result)
            {
                var userId = a.EventData;
                TimeOnly timeInFrom = new();
                TimeOnly timeInTo = new();
                TimeOnly timeOutFrom = new();
                TimeOnly timeOutTo = new();
                TimeOnly breakTimeFrom = new();
                TimeOnly breakTimeTo = new();
                int duration = new();


                if (a.When.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (!CheckSunday)
                    {
                        continue;
                    }
                    else
                    {
                        timeInFrom = new TimeOnly(0, 1);
                        timeInTo = new TimeOnly(23, 59);
                        timeOutFrom = new TimeOnly(0, 1);
                        timeOutTo = new TimeOnly(23, 59);
                    }

                }
                else if (a.When.DayOfWeek == DayOfWeek.Saturday)
                {
                    if (!CheckSaturday)
                    {
                        continue;
                    }
                    else
                    {
                        timeInFrom = new TimeOnly(0, 1);
                        timeInTo = new TimeOnly(23, 59);
                        timeOutFrom = new TimeOnly(0, 1);
                        timeOutTo = new TimeOnly(23, 59);
                    }
                }
                else if (a.When.DayOfWeek == DayOfWeek.Friday)
                {
                    timeInFrom = new TimeOnly(int.Parse(TimeInFrom.Substring(0, 2)), int.Parse(TimeInFrom[^2..]));
                    timeInTo = new TimeOnly(int.Parse(TimeInTo.Substring(0, 2)), int.Parse(TimeInTo[^2..]));
                    timeOutFrom = new TimeOnly(int.Parse(TimeOutFrom.Substring(0, 2)), int.Parse(TimeOutFrom[^2..]));
                    timeOutTo = new TimeOnly(int.Parse(TimeOutTo.Substring(0, 2)), int.Parse(TimeOutTo[^2..]));

                    breakTimeFrom = new TimeOnly(int.Parse(BreakTimeFriFrom.Substring(0, 2)), int.Parse(BreakTimeFriFrom[^2..]));
                    breakTimeTo = new TimeOnly(int.Parse(BreakTimeFriTo.Substring(0, 2)), int.Parse(BreakTimeFriTo[^2..]));
                    duration = int.Parse(BreakTimeFriDuration);
                }
                else
                {
                    timeInFrom = new TimeOnly(int.Parse(TimeInFrom.Substring(0, 2)), int.Parse(TimeInFrom[^2..]));
                    timeInTo = new TimeOnly(int.Parse(TimeInTo.Substring(0, 2)), int.Parse(TimeInTo[^2..]));
                    timeOutFrom = new TimeOnly(int.Parse(TimeOutFrom.Substring(0, 2)), int.Parse(TimeOutFrom[^2..]));
                    timeOutTo = new TimeOnly(int.Parse(TimeOutTo.Substring(0, 2)), int.Parse(TimeOutTo[^2..]));

                    breakTimeFrom = new TimeOnly(int.Parse(BreakTimeFrom.Substring(0, 2)), int.Parse(BreakTimeFrom[^2..]));
                    breakTimeTo = new TimeOnly(int.Parse(BreakTimeTo.Substring(0, 2)), int.Parse(BreakTimeTo[^2..]));
                    duration = int.Parse(BreakTimeDuration);
                }

                RecordModel rep = recordInCheck.Where(person => person.UserNumber == a.EventData.ToString()).FirstOrDefault();

                if (rep == null) //cannot find the user in recordInCheck
                {
                    //then create new records
                    rep = new RecordModel();
                    rep.UserNumber = a.EventData.ToString();
                    rep.Name = a.OriginalForename + " " + a.OriginalSurname;
                    rep.Remarks = "";

                    //Check rules  here

                }


                if (inputDoorNumber.Contains(a.Door)) //meaning it is inside 
                {

                    //if (rep.TimeIn != DateTime.MinValue)
                    //{
                    //    rep.TimeIn2 = a.When;
                    //}
                    if (rep.TimeIn != DateTime.MinValue)
                    {
                        continue;
                    }
                    else
                    {
                        rep.TimeIn = a.When;
                    }
                    recordInCheck.Add(rep);
                }
                else if (outputDoorNumber.Contains(a.Door)) //meaning it is outside
                {
                    if (rep.TimeIn == DateTime.MinValue)
                    {
                        continue;
                    }

                    rep.TimeOut = a.When;
                    rep.TotalHours = (rep.TimeOut - rep.TimeIn);

                    //if ((rep.TimeOut - rep.TimeIn) < new TimeSpan(0, int.Parse(DwellTime), 0))
                    //{
                    //    rep.TotalHours = new TimeSpan(0, 0, 0);
                    //    rep.Remarks = rep.Remarks + "Less than Dwell Time. ";
                    //}
                    //else
                    //{
                    //    rep.TotalHours = rep.TotalHours + (rep.TimeOut - rep.TimeIn);
                    //}

                    recordResult.Add(rep);

                    recordInCheck.Remove(rep);

                }

            }


            //checking and flushing the unpair records
            while (recordInCheck.Count > 0)
            {
                recordInCheck[0].TimeOut = new DateTime(DateOnly.FromDateTime(recordInCheck[0].TimeIn), new TimeOnly(23, 59, 0));
                recordInCheck[0].TotalHours = recordInCheck[0].TimeOut - recordInCheck[0].TimeIn;  //CheckDwellTime(recordInCheck[0].TimeIn, recordInCheck[0].TimeOut, recordInCheck[0].TotalHours);
                recordResult.Add(recordInCheck[0]);
                recordInCheck.RemoveAt(0);
            }

            var listResult = recordResult.OrderBy(r => r.TimeIn).ToList();

            recordResult = new ObservableCollection<RecordModel>(listResult);




            var userRange = recordResult.Select(n => n.UserNumber).Distinct().ToList();
            var dateRange = recordResult.OrderBy(d => d.TimeIn).Select(n => DateOnly.FromDateTime(n.TimeIn.Date)).Distinct().ToList();

            //Applying the rules
            foreach (DateOnly d in dateRange)
            {
                switch (d.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                    case DayOfWeek.Saturday:
                        timeInFrom = new TimeOnly(0, 1);
                        timeInTo = new TimeOnly(23, 59);
                        timeOutFrom = new TimeOnly(0, 1);
                        timeOutTo = new TimeOnly(23, 59);

                        break;
                    case DayOfWeek.Friday:
                        timeInFrom = new TimeOnly(int.Parse(TimeInFrom.Substring(0, 2)), int.Parse(TimeInFrom[^2..]));
                        timeInTo = new TimeOnly(int.Parse(TimeInTo.Substring(0, 2)), int.Parse(TimeInTo[^2..]));
                        timeOutFrom = new TimeOnly(int.Parse(TimeOutFrom.Substring(0, 2)), int.Parse(TimeOutFrom[^2..]));
                        timeOutTo = new TimeOnly(int.Parse(TimeOutTo.Substring(0, 2)), int.Parse(TimeOutTo[^2..]));
                        break;
                    default:
                        timeInFrom = new TimeOnly(int.Parse(TimeInFrom.Substring(0, 2)), int.Parse(TimeInFrom[^2..]));
                        timeInTo = new TimeOnly(int.Parse(TimeInTo.Substring(0, 2)), int.Parse(TimeInTo[^2..]));
                        timeOutFrom = new TimeOnly(int.Parse(TimeOutFrom.Substring(0, 2)), int.Parse(TimeOutFrom[^2..]));
                        timeOutTo = new TimeOnly(int.Parse(TimeOutTo.Substring(0, 2)), int.Parse(TimeOutTo[^2..]));
                        break;
                }


                foreach (string n in userRange)
                {
                    var listCheck = listResult.Where(r => (r.UserNumber == n) && (DateOnly.FromDateTime(r.TimeIn) == d)).ToList();
                    //recordcheck contains all the transactions of the day for the user
                    DateTime lastTimeOut = DateTime.MinValue;

                    if (listCheck.Count > 0)
                    {
                        foreach (var l in listCheck)
                        { 
                            RecordModel record = new RecordModel();
                            //RecordModel nextRecord = new RecordModel();


                            //Checking the TimeIn Rule for late
                            if (listCheck.IndexOf(l) == 0)
                            {
                                if (TimeOnly.FromDateTime(l.TimeIn) < timeInFrom) //checking the first morning
                                {
                                    l.TotalHours = l.TotalHours + ( l.TimeIn - (new DateTime(DateOnly.FromDateTime(l.TimeIn), timeInFrom))); //coming in earlier
                                }
                                else if (TimeOnly.FromDateTime(l.TimeIn) > timeInTo)
                                {
                                    l.Remarks = l.Remarks + "Late. ";
                                }
                                
                              
                                

                                //RecordModel record = recordResult.Where(r => (r.UserNumber == n) || r.TimeIn == listCheck[0].TimeIn).FirstOrDefault();
                                //if (record != null) { record.Remarks = listCheck[0].Remarks; }
                            }

                            
                            record = recordResult.Where(r => (r.UserNumber == n) && r.TimeIn == l.TimeIn).FirstOrDefault();
                            if (record != null)
                            {
                                record.Remarks = l.Remarks;
                            }

                            var t = l.TimeIn - lastTimeOut;

                            if (t < new TimeSpan(0, int.Parse(GracePeriod),1))
                            {
                                l.TotalHours = l.TotalHours + t;
                            }

                            lastTimeOut = l.TimeOut;


                            //Check Dwell Time
                            if (l.TotalHours < new TimeSpan(0, int.Parse(DwellTime), 1))
                            {
                                l.TotalHours = new TimeSpan(0, 0, 0);
                                l.Remarks = l.Remarks + "Less than Dwell Time. ";
                            }

                        }
                    }

                }

            }
        }


            
        

        //public TimeSpan CheckDwellTime(DateTime inTime, DateTime outTime, TimeSpan calculatedHours)
        //{
        //    if ((outTime - inTime) < new TimeSpan(0, int.Parse(DwellTime), 0))
        //    {
        //        calculatedHours = new TimeSpan(0, 0, 0);
                
        //    }
        //    else
        //    {
        //        calculatedHours = calculatedHours + (outTime - inTime);
        //    }

        //    return calculatedHours;
        //}

        public ObservableCollection<RecordModel> GetResults()
        {
            return recordResult;
        }
        private ObservableCollection<EventLogModel> Samplerun()
        {
            SampleModel test = new();
            return test.AddSample();
            
        }

        private DateTime CheckDay(DateTime dt)
        {
            dt = dt.AddDays(1);
            return dt;
        }

    }
}
