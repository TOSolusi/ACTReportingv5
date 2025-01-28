using ACTReportingTools.Helpers;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ACTReportingTools.ViewModels
{
    public class SettingsViewModel : Screen
    {

        public JObject SettingsConfig { get; set; }
        public string FileSettings { get; set; }
        public MenuViewModel menuViewModel { get; set; }
        public string ConnString { get; set; }
        public SQLDataAccess daAccess { get; set; }
        public SettingsViewModel()
        {
            menuViewModel = IoC.Get<MenuViewModel>();
            FileSettings = IoC.Get<FileLocationViewModel>().FileSettings;

            SettingsConfig = ConfigHelper.LoadConfig(FileSettings);

            StringServer = (string)SettingsConfig["Server"];
            StringDatabase = (string)SettingsConfig["Database"];

            //ConnString = $"Server={StringServer};Database={StringDatabase}; Integrated Security=true; Encrypt=false;";

            

        }

        private string stringServer;

        public string StringServer
        {
            get { return stringServer; }
            set
            {
                stringServer = value;
                NotifyOfPropertyChange(() => StringServer);
                ConnString = $"Server={StringServer};Database={StringDatabase}; Integrated Security=true; Encrypt=false;";
            }
        }

        private string stringDatabase;

        public string StringDatabase
        {
            get { return stringDatabase; }
            set
            {
                stringDatabase = value;
                NotifyOfPropertyChange(() => StringDatabase);
                ConnString = $"Server={StringServer};Database={StringDatabase}; Integrated Security=true; Encrypt=false;";
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

        public async Task BtnTestConnect()
        {
            //ConnString = $"Server={StringServer};Database={StringDatabase}; Integrated Security=true; Encrypt=false;";
            daAccess = new SQLDataAccess(connectionString: ConnString);
            await daAccess.TestConnection();
        }

        public void BtnCancel()
        {
            menuViewModel.ButtonReportDate();
        }

        public void BtnSaveSettings()
        {
            SettingsConfig["Server"] = StringServer;
            SettingsConfig["Database"] = StringDatabase;
            
            ConfigHelper.SetConfig(SettingsConfig, FileSettings);
            MessageBox.Show("Settings Saved");
            menuViewModel.ButtonReportDate();
        }
    }
}
