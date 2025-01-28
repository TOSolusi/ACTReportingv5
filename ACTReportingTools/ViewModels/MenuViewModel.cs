using ACTReportingTools.Helpers;
using Caliburn.Micro;
using Syncfusion.UI.Xaml.NavigationDrawer;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Action = System.Action;

namespace ACTReportingTools.ViewModels
{
    public class MenuViewModel : Screen
    {
        private ShellViewModel contentModel;

        public ShellViewModel ContentModel
        {
            get { return contentModel; }
            set
            {
                contentModel = value;
                NotifyOfPropertyChange(() => ContentModel);
            }
        }

       
        
        private string title;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public ICommand CommandReportDate { get; }

        //private ICommand commandReportDate;

        //public ICommand CommandReportDate
        //{
        //    get
        //    {
        //        if (commandReportDate == null)
        //            commandReportDate = new RelayCommand(args => ButtonReportDate());
        //        return commandReportDate;
        //    }

        //}


        public ICommand CommandReportSettings { get; }
        //private ICommand commandReportSettings;

        //public ICommand CommandReportSettings
        //{
        //    get
        //    {
        //        if (commandReportSettings == null)
        //            commandReportSettings = new RelayCommand(args => ButtonReportSettings());
        //        return commandReportSettings;
        //    }

        //}
        public ICommand CommandSettings { get; }
        public ICommand CommandExit { get; }
        //private ICommand commandExit;
        //public ICommand CommandExit
        //{
        //    get
        //    {
        //        if (commandExit == null)
        //            commandExit = new RelayCommand(args => ButtonExit());
        //        return commandExit;
        //    }

        //}
        public string  FileNameReportSettings { get; set; } 

        public MenuViewModel()
        {
            ContentModel = IoC.Get<ShellViewModel>();
            Title = "Menu Options";

            //FileNameReportSettings = FileReportSettings;

            CommandReportDate = new RelayCommand(ButtonReportDate);
            CommandReportSettings = new RelayCommand(ButtonReportSettings);
            CommandSettings = new RelayCommand(ButtonSettings);
            CommandExit = new RelayCommand(ButtonExit);


        }

        public void ButtonReportDate()
        {
            ContentModel.MainContent = IoC.Get<ReportDateViewModel>();
        }

        public void ButtonReportSettings()
        {
            ContentModel.MainContent = IoC.Get<ReportSettingsViewModel>();
        }

        public void ButtonSettings()
        {
            ContentModel.MainContent = IoC.Get<SettingsViewModel>();
        }

        public void ButtonExit()
        {
            Application.Current.Shutdown();
        }
        
       
    }
}
