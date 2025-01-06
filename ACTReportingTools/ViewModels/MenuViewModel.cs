using Caliburn.Micro;
using Syncfusion.UI.Xaml.NavigationDrawer;
using System;
using System.Collections.Generic;
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

        private object selectedItem;
        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        public ICommand NavigateCommand { get; }

        public MenuViewModel()
        {
            ContentModel = IoC.Get<ShellViewModel>();
            Title = "Menu Options";

            NavigateCommand = new RelayCommand(Button2);
        }




        public async Task Button1()
        {
            // Add login logic here
            // For simplicity, let's just display a message for now

            ContentModel.MainContent = IoC.Get<ReportDateViewModel>();

        }

        public void Button2()
        {
            // Add login logic here
            // For simplicity, let's just display a message for now
            ContentModel.MainContent = IoC.Get<Content1ViewModel>();
        }

        public async Task Button3()
        {
            // Add login logic here
            // For simplicity, let's just display a message for now
            ContentModel.MainContent = IoC.Get<Content2ViewModel>();
        }


        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;
            public RelayCommand(Action execute, Func<bool> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }
            public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
            public void Execute(object parameter) => _execute();
            public event EventHandler CanExecuteChanged;

        }
    }
}
