using ACTReportingTools.ViewModels;
using Caliburn.Micro;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ACTReportingTools.Helpers
{
    public class SQLDataAccess
    {
        public string StringServer { get; set; }

        public string StringDatabase { get; set; }
        public string connString { get; set; }
        public JObject SettingsConfig { get; set; }
        public string FileSettings { get; set; }

        public SQLDataAccess()
        {
            FileSettings = IoC.Get<FileLocationViewModel>().FileSettings;

            SettingsConfig = ConfigHelper.LoadConfig(FileSettings);

            StringServer = (string)SettingsConfig["Server"];
            StringDatabase = (string)SettingsConfig["Database"];

            connString = $"Server={StringServer};Database={StringDatabase}; Integrated Security=true; Encrypt=false;";
        }

        public SQLDataAccess(string connectionString)
        {
            connString = connectionString;

           
        }

        public async Task TestConnection()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Connection Successful");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection Failed due to {ex.Message}");
                }
            }
        }
    }
}
