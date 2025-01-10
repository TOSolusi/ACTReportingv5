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
        public ICommand CommandReportSettings { get; }
        public ICommand CommandSettings { get; }
        public ICommand CommandExit { get; }

        
        public MenuViewModel()
        {
            ContentModel = IoC.Get<ShellViewModel>();
            Title = "Menu Options";

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
            ContentModel.MainContent = IoC.Get<ReportDateViewModel>();
        }

        public void ButtonExit()
        {
            Application.Current.Shutdown();
        }
        
       
    }
}
