using ACTReportingTools.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.Helpers
{
    public class LogServiceHelper
    {

        private readonly HttpClient _httpClient;

        public LogServiceHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<ObservableCollection<UserFileModel>> GetUsersListAsync(string serverAddress)
        //{
        //    if (string.IsNullOrEmpty(serverAddress))
        //    {
        //        throw new ArgumentException("Server address cannot be null or empty.", nameof(serverAddress));
        //    }
        //    // Ensure the server address is valid and starts with "http://" or "https://"
        //    var response = await _httpClient.GetAsync($"http://{serverAddress}/api/ACTData/getUsersList");

        //    response.EnsureSuccessStatusCode();

        //    var users = await response.Content.ReadFromJsonAsync<ObservableCollection<UserFileModel>>();
        //    return users ?? new ObservableCollection<UserFileModel>();
        //}

        public async Task<ObservableCollection<EventLogModel>> GetEventLogsAsync(string serverAddress, string StartDate, string EndDate)
        {
            if (string.IsNullOrEmpty(serverAddress))
            {
                throw new ArgumentException("Server address cannot be null or empty.", nameof(serverAddress));
            }

          
            var response = await _httpClient.GetAsync($"http://{serverAddress}/api/ACTData/getEventLog?startDate={StartDate}&finishDate={EndDate}");
            response.EnsureSuccessStatusCode();
            var eventLogs = await response.Content.ReadFromJsonAsync<ObservableCollection<EventLogModel>>();
            return eventLogs ?? new ObservableCollection<EventLogModel>();
        }
    }
}
