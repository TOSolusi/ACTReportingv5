using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.ViewModels
{
    public class ShellViewModel : Conductor<Object>
    {
        public string fileReportSettings { get; set; }

       

        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            //await UserLogin();
            //fileReportSettings = "Settings/ReportSettings.json";

            await LoadMenu();
            await LoadContent();
           

        }



        private IWindowManager _windowManager;

        private IScreen _mainContent;
        public IScreen MainContent
        {
            get { return _mainContent; }
            set
            {
                _mainContent = value;
                NotifyOfPropertyChange(() => MainContent);
            
            }
        }

        public ShellViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
        public async Task LoadMenu()
        {
            var menuModel = IoC.Get<MenuViewModel>();
            await ActivateItemAsync(menuModel, new CancellationToken());
        }

        public async Task LoadContent()
        {
            var contentModel = IoC.Get<ReportDateViewModel>();
            //await ActivateItemAsync(contentModel, new CancellationToken());
            MainContent = contentModel;
        }

        
    }
}
