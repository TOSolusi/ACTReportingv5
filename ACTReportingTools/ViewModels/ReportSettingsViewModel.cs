using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.ViewModels
{
    public class ReportSettingsViewModel : Screen
    {
        public MenuViewModel menuViewModel { get; set; }         
        public ReportSettingsViewModel()
        {
                menuViewModel = IoC.Get<MenuViewModel>();
        }

        public void ButtonCancel()
        {
            menuViewModel.ButtonReportDate();
        }
    }
}
