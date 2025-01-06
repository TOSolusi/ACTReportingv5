using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.ViewModels
{
    public class ReportDateViewModel : Screen
    {

        private IWindowManager _windowManager;
        public ReportDateViewModel(IWindowManager windowManager)
        {
                _windowManager = windowManager;
        }

        public async Task BtnGenerateReport()
        {

            await _windowManager.ShowWindowAsync(new ReportPreviewViewModel());
         }
    }
}
