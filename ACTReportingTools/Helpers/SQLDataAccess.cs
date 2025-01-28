using Microsoft.Data.SqlClient;
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
        public string connString { get; set; }
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
