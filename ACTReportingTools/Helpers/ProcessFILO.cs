using ACTReportingTools.Models;
using ACTReportingTools.ViewModels;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.Helpers
{
    public class ProcessFILO
    {
        public string _startDate { get; set; }
        public string _endDate { get; set; }
        public JObject SettingsConfig { get; set; }
        public string TimeInFrom { get; set; }
        public string TimeInTo { get; set; }
        public string sqlCommand { get; set; }
        public string doorInList { get; set; }
        public string doorOutList { get; set; }
        ObservableCollection<RecordModel> recordResult { get; set; }
        ObservableCollection<RecordModel> recordInCheck { get; set; }
        public string FileReportSettings { get; set; }
        public string ServerAddress { get; set; }
        public string ControllerNumbers { get; set; }

        // Rename the method to avoid conflict with the class name
        public async Task ProcessFILOASync(string startDate, string endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
            FileReportSettings = IoC.Get<FileLocationViewModel>().FileSettings;
            SettingsConfig = ConfigHelper.LoadConfig(FileReportSettings);
            
            doorInList = (string)SettingsConfig["INDoorNumbers"] ; //string.Join(",", DoorIn);
            doorOutList = (string)SettingsConfig["OUTDoorNumbers"]; //string.Join(",", DoorOut);
            TimeInFrom = (string)SettingsConfig["TimeInFrom"];
            TimeInTo = (string)SettingsConfig["TimeInTo"];
            ServerAddress = (string)SettingsConfig["ServerAddress"];
            ControllerNumbers = (string)SettingsConfig["ControllerNumbers"];

            List<int> doorIn = doorInList.Split(',').Select(int.Parse).ToList();
            List<int> doorOut = doorOutList.Split(',').Select(int.Parse).ToList();
            List<int> controllerSet = ControllerNumbers.Split(',').Select(int.Parse).ToList();

            //getting data from Server either from SQL or from network
            //SQLDataAccess daAccess = new SQLDataAccess();
            //var result = daAccess.GetLogReport(startDate, endDate);

            //getting data from API
            var httpClient = new HttpClient();
            var userService = new UserServiceHelper(httpClient);
            var users = await userService.GetUsersWithGroupNameAsync(ServerAddress);
            var logService = new LogServiceHelper(httpClient);
            var result = await logService.GetEventLogsAsync(ServerAddress, _startDate, _endDate);

            //result = result.Where(r => doorInList.Contains(r.Door));
            result = result.Where(r => ((doorIn.Contains(r.Door)) || (doorOut.Contains(r.Door)))
                 && (controllerSet.Contains(r.Controller))).ToObservableCollection<EventLogModel>();
         


            // After getting 'result' from daAccess.GetLogReport(startDate, endDate)
            var userNumbers = result.Select(r => r.EventData.ToString()).Distinct().ToList();
            //var users = daAccess.GetUsers(userNumbers); // Assume this returns a collection of UserModel with UserNumber and Name
            //var userGroups = users.ToDictionary(u => u.UserNumber, u => daAccess.GetGroupName(u.UserNumber));
            

            recordResult = new();

            // Group by user and date
            recordResult = result
                .GroupBy(r => new {  r.EventData, Date = r.When.Date })
                .Select(g =>
                {
                    var firstIn = g.Min(r => r.When);
                    var lastOut = g.Max(r => r.When);
                    var anyRecord = g.First();
                    //var userNumber = anyRecord.EventData.ToString();
                    //var name = $"{anyRecord.OriginalForename} {anyRecord.OriginalSurname}";
                    //var group = userGroups.ContainsKey(userNumber) ? userGroups[userNumber] : "";


                    return new RecordModel
                    {
                        UserNumber = anyRecord.EventData.ToString(),
                        Name = $"{anyRecord.OriginalForename} {anyRecord.OriginalSurname}",
                        Group = users.FirstOrDefault(u => u.UserNumber == anyRecord.EventData.ToString())?.UserGroup ?? "",
                        TimeIn = firstIn,
                        TimeOut = lastOut,
                        DailyTotal = lastOut - firstIn,
                        //Remarks = $"FILO: {firstIn:t} - {lastOut:t} ({(lastOut - firstIn).TotalHours:F2} hrs)"
                    };
                })
                .OrderBy(r => r.UserNumber)
                .ThenBy(r => r.TimeIn)
                .ToObservableCollection();

            

            //return Results;
        }

        public async Task<ObservableCollection<RecordModel>> GetResults()
        {
            return recordResult.OrderBy(a => a.TimeIn).ToObservableCollection<RecordModel>();
        }
    }
}
