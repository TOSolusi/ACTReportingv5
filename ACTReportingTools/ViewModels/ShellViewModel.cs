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
        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            //await UserLogin();
            await LoadMenu();
            await LoadContent();
           

        }

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
