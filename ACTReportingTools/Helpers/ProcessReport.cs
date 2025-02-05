using ACTReportingTools.Models;
using ACTReportingTools.ViewModels;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using Syncfusion.Data.Extensions;
using Syncfusion.Windows.Controls;
using Syncfusion.XlsIO.Implementation;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Composition;
using System.Formats.Tar;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
        public string WorkDuration { get; set; }
        public string InOfficeBreak { get; set; }
        public TimeOnly timeInFrom { get; set; }
        public TimeOnly timeInTo { get; set; }
        public TimeOnly timeOutFrom { get; set; }
        public TimeOnly timeOutTo { get; set; }
        public TimeOnly breakTimeFrom { get; set; }
        public TimeOnly breakTimeTo { get; set; }
        public int breakDuration { get; set; }
        public int inOfficeDuration { get; set; }
        public string sqlCommand { get; set; }


        ObservableCollection<RecordModel> recordResult { get; set; }
        ObservableCollection<RecordModel> recordInCheck { get; set; }
        public string FileReportSettings { get; set; }
        public ProcessReport(string startDate, string endDate)
        {
            //_startDate = startDate.ToDateTime();
            //_endDate = endDate.ToDateTime() + new TimeSpan(23,59,59);

            //string p1 = _startDate.ToString("yyyy-MM-dd HH:mm:ss");
            //string p2 = _endDate.ToString("yyyy-MM-dd HH:mm:ss");

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
            WorkDuration = (string)SettingsConfig["WorkDuration"];
            InOfficeBreak = (string)SettingsConfig["InOfficeBreakTimeDuration"];
            //for test run
            //var result = Samplerun();
            //sqlCommand = $"Select * from Log where ( (\"When\" between '{p1}' and '{p2}') and ((Event=50) or (Event=52)) ) order by \"When\"";

            SQLDataAccess daAccess = new SQLDataAccess();
            var result = daAccess.GetLogReport(startDate, endDate);

            //start checking on date
            //List<DateTime> dateProcess = new List<DateTime>();
            //dateProcess = result.OrderBy(d => d.When.Date).Select(d => d.When.Date).Distinct().ToList();
           
            recordResult = new();

            int[] inputDoorNumber = { 1 };
            //int[] outputDoorNumber = { 2 };
           

                recordInCheck = new();

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

                    RecordModel rep = recordInCheck.Where(person => ((person.UserNumber == a.EventData.ToString()) && (person.TimeIn.Date ==  a.When.Date ))).FirstOrDefault();

                    if (rep == null) //cannot find the user in recordInCheck
                    {
                        //then create new records
                        rep = new RecordModel();
                        rep.UserNumber = a.EventData.ToString();
                        
                        rep.Name = a.OriginalForename + " " + a.OriginalSurname;
                        rep.Remarks = "";
                    }

                    if ((inputDoorNumber.Contains(a.Door)) && (a.Event == 50)) //meaning it is inside 
                    {
                                  
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
                    else if ((inputDoorNumber.Contains(a.Door)) && (a.Event == 52))//meaning it is outside
                    {
                        if (rep.TimeIn == DateTime.MinValue)
                        {
                            continue;
                        }

                        rep.TimeOut = a.When;
                        rep.TotalHours = (rep.TimeOut - rep.TimeIn);

                        recordResult.Add(rep);
                        recordInCheck.Remove(rep);
                    }
                }

                //checking and flushing the unpair records
                while (recordInCheck.Count > 0)
                {
                    //If not clock out, change to clock in.
                    recordInCheck[0].TimeOut = recordInCheck[0].TimeIn; //new DateTime(DateOnly.FromDateTime(recordInCheck[0].TimeIn), new TimeOnly(23, 59));
                    recordInCheck[0].TotalHours = recordInCheck[0].TimeOut - recordInCheck[0].TimeIn;  //CheckDwellTime(recordInCheck[0].TimeIn, recordInCheck[0].TimeOut, recordInCheck[0].TotalHours);
                    recordInCheck[0].Remarks = recordInCheck[0].Remarks + "No Clock Out. ";
                    recordResult.Add(recordInCheck[0]);
                    recordInCheck.RemoveAt(0);
                }


            
                var listResult = recordResult.OrderBy(r => r.TimeIn).ToList();

                recordResult = new ObservableCollection<RecordModel>(listResult);
                  
            List<string> userRange = recordResult.Select(n => n.UserNumber).Distinct().ToList();
            ObservableCollection<UserModel> users = new ObservableCollection<UserModel>();

            ObservableCollection<UserModel> userInfo = daAccess.GetUsers(userRange);


            //foreach (string n in userRange)
            //{
            //    UserModel user = new UserModel();
            //    user.UserNumber = n;
            //    user.Name = recordResult.Where(r => r.UserNumber == n).Select(r => r.Name).FirstOrDefault();
            //    user.Group = recordResult.Where(r => r.UserNumber == n).Select(r => r.Group).FirstOrDefault();
            //    users.Add(user);
            //}

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
                        timeOutFrom = new TimeOnly(0, 1 );
                        timeOutTo = new TimeOnly(23, 59);

                        breakTimeFrom = new TimeOnly(0, 1);
                        breakTimeTo = new TimeOnly(23, 59);
                        breakDuration = 0;
                        inOfficeDuration = 0;
                        break;
                    case DayOfWeek.Friday:
                        timeInFrom = new TimeOnly(int.Parse(TimeInFrom.Substring(0, 2)), int.Parse(TimeInFrom[^2..]));
                        timeInTo = new TimeOnly(int.Parse(TimeInTo.Substring(0, 2)), int.Parse(TimeInTo[^2..]));
                        timeOutFrom = new TimeOnly(int.Parse(TimeOutFrom.Substring(0, 2)), int.Parse(TimeOutFrom[^2..]));
                        timeOutTo = new TimeOnly(int.Parse(TimeOutTo.Substring(0, 2)), int.Parse(TimeOutTo[^2..]));

                        breakTimeFrom = new TimeOnly(int.Parse(BreakTimeFriFrom.Substring(0, 2)), int.Parse(BreakTimeFriFrom[^2..]));
                        breakTimeTo = new TimeOnly(int.Parse(BreakTimeFriTo.Substring(0, 2)), int.Parse(BreakTimeFriTo[^2..]));
                        breakDuration = int.Parse(BreakTimeFriDuration);
                        inOfficeDuration = int.Parse(InOfficeBreak);
                        break;
                    default:
                        timeInFrom = new TimeOnly(int.Parse(TimeInFrom.Substring(0, 2)), int.Parse(TimeInFrom[^2..]));
                        timeInTo = new TimeOnly(int.Parse(TimeInTo.Substring(0, 2)), int.Parse(TimeInTo[^2..]));
                        timeOutFrom = new TimeOnly(int.Parse(TimeOutFrom.Substring(0, 2)), int.Parse(TimeOutFrom[^2..]));
                        timeOutTo = new TimeOnly(int.Parse(TimeOutTo.Substring(0, 2)), int.Parse(TimeOutTo[^2..]));

                        breakTimeFrom = new TimeOnly(int.Parse(BreakTimeFrom.Substring(0, 2)), int.Parse(BreakTimeFrom[^2..]));
                        breakTimeTo = new TimeOnly(int.Parse(BreakTimeTo.Substring(0, 2)), int.Parse(BreakTimeTo[^2..]));
                        breakDuration = int.Parse(BreakTimeDuration);
                        inOfficeDuration = int.Parse(InOfficeBreak);
                        break;
                }


                foreach (string n in userRange)
                {
                    var listCheck = listResult.Where(r => (r.UserNumber == n) && (DateOnly.FromDateTime(r.TimeIn) == d)).ToList();
                    //recordcheck contains all the transactions of the day for the user
                    DateTime lastTimeOut = new(0);
                   
                    TimeSpan weeklyTotalHours = new(0);

                    if (listCheck.Count > 0)
                    {
                        foreach (var l in listCheck)
                        {
                            RecordModel record = new RecordModel();
                            //RecordModel nextRecord = new RecordModel();

                            l.Group = userInfo.Where(u => u.UserNumber == n).Select(u => u.UserGroup).FirstOrDefault();

                            //Checking the TimeIn Rule for late
                            if (listCheck.IndexOf(l) == 0)
                            {
                                if (TimeOnly.FromDateTime(l.TimeIn) < timeInFrom) //checking the first morning
                                {
                                    l.TotalHours = l.TotalHours + (l.TimeIn - (new DateTime(DateOnly.FromDateTime(l.TimeIn), timeInFrom))); //coming in earlier
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


                               


                                //Rule 1. Grace Period
                                var t =  l.TimeIn - lastTimeOut;

                                if (t < new TimeSpan(0, int.Parse(GracePeriod), 1))
                                {
                                    l.TotalHours = l.TotalHours + t;
                                }
                                
                                lastTimeOut = l.TimeOut;


                                //Rule 2. Check for Dwell Time
                                if (l.TotalHours < new TimeSpan(0, int.Parse(DwellTime), 1))
                                {
                                    l.TotalHours = new TimeSpan(0);
                                    l.Remarks = l.Remarks + "Less than Dwell Time. ";
                                }

                                ////Count Daily Total Hours
                                //dailyTotalHours = dailyTotalHours + l.TotalHours;

                                //if (listCheck.IndexOf(l) == (listCheck.Count - 1))
                                //{
                                //    record.DailyTotal = dailyTotalHours;
                                //    if (dailyTotalHours < new TimeSpan(int.Parse(WorkDuration.Substring(0, 2)), int.Parse(WorkDuration[^2..]), 1))
                                //    {
                                //        record.Remarks = record.Remarks + "Daily Total Hours Less than Recommended. \n";
                                //    }
                                //}


                            }
                        }
                    }


                    //Checking for lunch break

                    var lunchCheck = listResult.Where(
                        r => (r.UserNumber == n) &&
                        (DateOnly.FromDateTime(r.TimeIn) == d) &&
                        (((TimeOnly.FromDateTime(r.TimeIn) >= breakTimeFrom) && (TimeOnly.FromDateTime(r.TimeIn) <= breakTimeTo)) ||
                        ((TimeOnly.FromDateTime(r.TimeOut) >= breakTimeFrom) && (TimeOnly.FromDateTime(r.TimeOut) <= breakTimeTo)))).ToList();

                    if (lunchCheck.Count > 0)
                    {
                        {
                            TimeSpan totalInOfficeDuringLunch = new TimeSpan(0);
                            var lunchRemark = "";
                            foreach (var l in lunchCheck)
                            {
                                if (TimeOnly.FromDateTime(l.TimeIn) < breakTimeFrom)
                                {
                                    totalInOfficeDuringLunch = totalInOfficeDuringLunch + (TimeOnly.FromDateTime(l.TimeOut) - breakTimeFrom);
                                }
                                else if (TimeOnly.FromDateTime(l.TimeOut) > breakTimeTo)
                                {
                                    totalInOfficeDuringLunch = totalInOfficeDuringLunch + (breakTimeTo - (TimeOnly.FromDateTime(l.TimeIn)));
                                }
                                else
                                {
                                    //totalLunchDuration = totalLunchDuration + l.TotalHours;
                                    totalInOfficeDuringLunch = totalInOfficeDuringLunch + (TimeOnly.FromDateTime(l.TimeOut) - TimeOnly.FromDateTime(l.TimeIn));
                                }


                                
                            }

                            var durationOutsideOffice = breakTimeTo - breakTimeFrom - totalInOfficeDuringLunch;

                            if (durationOutsideOffice < new TimeSpan(0, inOfficeDuration, 0))//new TimeSpan(0, inOfficeDuration, 0))
                            {
                                totalInOfficeDuringLunch = new TimeSpan(0, inOfficeDuration, 0).Negate() + durationOutsideOffice;
                            }
                            else if (durationOutsideOffice < new TimeSpan(0, breakDuration, 0))
                            {
                                totalInOfficeDuringLunch = new TimeSpan(0, breakDuration, 0).Negate() + totalInOfficeDuringLunch;
                            }
                            else
                            {
                                totalInOfficeDuringLunch = durationOutsideOffice.Negate();
                                lunchRemark = "Lunch Break too long. ";
                            }
                            //Creating new lunch record
                            var lunchRecord = new RecordModel();
                                lunchRecord.UserNumber = n;
                                lunchRecord.Name = userInfo.Where(u => u.UserNumber == n).Select(u => u.Name).FirstOrDefault();
                                lunchRecord.Group = userInfo.Where(u => u.UserNumber == n).Select(u => u.UserGroup).FirstOrDefault();
                                lunchRecord.TimeIn = new DateTime(d, breakTimeFrom);
                                lunchRecord.TimeOut = new DateTime(d, breakTimeTo);
                                lunchRecord.TotalHours = totalInOfficeDuringLunch;
                                lunchRecord.Remarks = lunchRemark;
                                recordResult.Add(lunchRecord);
                            
                        }
                    }
                    else if (lunchCheck.Count == 0)
                    {
                        var lunchCheckForLong = listResult.Where(
                        r => (r.UserNumber == n) &&
                        (DateOnly.FromDateTime(r.TimeIn) == d) &&
                        ((TimeOnly.FromDateTime(r.TimeIn) <= breakTimeFrom) && (TimeOnly.FromDateTime(r.TimeOut) >= breakTimeTo))).FirstOrDefault();

                        if (lunchCheckForLong != null)
                        {
                            var lunchRecord = new RecordModel();
                            lunchRecord.UserNumber = n;
                            lunchRecord.Name = userInfo.Where(u => u.UserNumber == n).Select(u => u.Name).FirstOrDefault();
                            lunchRecord.Group = userInfo.Where(u => u.UserNumber == n).Select(u => u.UserGroup).FirstOrDefault();
                            lunchRecord.TimeIn = new DateTime(d, breakTimeFrom);
                            lunchRecord.TimeOut = new DateTime(d, breakTimeTo);
                            lunchRecord.TotalHours = new TimeSpan(0, inOfficeDuration, 0).Negate();
                            //lunchRecord.Remarks = "No Lunch Break. \n";
                            recordResult.Add(lunchRecord);
                        }


                    }


                    //reiterate the list to check for daily total

                    TimeSpan dailyTotalHours = new(0);
                    listCheck = recordResult.Where(r => (r.UserNumber == n) && (DateOnly.FromDateTime(r.TimeIn) == d)).OrderBy(r => r.TimeIn).ToList();

                    if (listCheck.Count > 0)
                    {
                        foreach (var l in listCheck)
                        {
                            RecordModel record = new RecordModel();
                            record = recordResult.Where(r => (r.UserNumber == n) && r.TimeIn == l.TimeIn).FirstOrDefault();
                            //Count Daily Total Hours
                            dailyTotalHours = dailyTotalHours + l.TotalHours;

                            if (listCheck.IndexOf(l) == (listCheck.Count - 1))
                            {
                                record.DailyTotal = dailyTotalHours;
                                if (dailyTotalHours < new TimeSpan(int.Parse(WorkDuration.Substring(0, 2)), int.Parse(WorkDuration[^2..]), 1))
                                {
                                    record.Remarks = record.Remarks + "Daily Total Hours Less than Recommended. ";
                                }
                            }
                        }
                    }
                    //((TimeOnly.FromDateTime(r.TimeIn) >= breakTimeFrom) && (TimeOnly.FromDateTime(r.TimeIn) <= breakTimeTo)) ||
                    //((TimeOnly.FromDateTime(r.TimeOut) >= breakTimeFrom) && (TimeOnly.FromDateTime(r.TimeOut) <= breakTimeTo))).ToList();



                    //( breakTimeFrom <  (TimeOnly.FromDateTime(r.TimeIn)) < breakTimeTo) 
                    //|| (TimeOnly.FromDateTime(r.TimeOut) between breakTimeFrom and breakTimeTo))
                    //).ToList();
                    //(TimeOnly.FromDateTime(r.TimeIn) >= breakTimeFrom) && (TimeOnly.FromDateTime(r.TimeIn) <= breakTimeTo)).ToList();
                }


            }

            //GetWeeklyTotal(dateRange);
        }

        



        public ObservableCollection<RecordModel> GetResults()
        {
            return recordResult.OrderBy(a => a.TimeIn).ToObservableCollection<RecordModel>();
        }
        private ObservableCollection<EventLogModel> Samplerun()
        {
            SampleModel test = new();
            return test.AddSample();
            
        }

        //private void GetWeeklyTotal(List<DateOnly> DateRange)
        //{
        //    foreach (DateTime in DateRange)
        //    {

        //    }
        //}

        private DateTime CheckDay(DateTime dt)
        {
            dt = dt.AddDays(1);
            return dt;
        }

    }
}
