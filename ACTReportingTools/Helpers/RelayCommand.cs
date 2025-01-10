using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace ACTReportingTools.Helpers
{

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private bool _canperformaction = true;
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();
        public event EventHandler CanExecuteChanged;

        public bool CanPerformAction
        {
            get
            {
                return _canperformaction;
            }
            set
            {
                _canperformaction = value;
                this.ClickCommand.RaiseCanExecuteChanged();
                this.OnPropertyChanged("CanPerformAction");
            }
        }

        private bool CanPerformClickAction(object parameter)
        {
            return CanPerformAction;
        }

        public DelegateCommand<object> ClickCommand { get; set; }

        private void ClickAction(object parameter)
        {
            MessageBox.Show(parameter.ToString() + " item has been clicked");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
