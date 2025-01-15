using ACTReportingTools.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ACTReportingTools
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();



        }

        protected override void Configure()
        {
            _container.Instance(_container)
                ;

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ShellViewModel, ShellViewModel>()
                .Singleton<MenuViewModel, MenuViewModel>()
                .Singleton<FileLocationViewModel, FileLocationViewModel>()
                ;


            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {

            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqUE1hXk5Hd0BLVGpAblJ3T2ZQdVt5ZDU7a15RRnVfR19iSXdSd0diW35feQ==;Mgo+DSMBPh8sVXJ1S0R+WVpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jTHxSdkxiWH1fc3VcTw==;ORg4AjUWIQA/Gnt2VFhiQlRPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtRd0VqWH5ccXBXT2k=;MjEzNDA2MUAzMjMxMmUzMjJlMzVuMHoxdGdrWjRvN1N1YldqOGdUUHl3M1VFMnBkOXVydnBjMXhiQlFGNmxBPQ==;MjEzNDA2MkAzMjMxMmUzMjJlMzVNdmk4TVpBUnA5RVAyY3Rudm1HeTZoSTVZMmhLd2xtSlZ2SU9lN2F2RmF3PQ==;NRAiBiAaIQQuGjN/V0d+Xk9BfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5WdERjUH5fcnVcQWRd;MjEzNDA2NEAzMjMxMmUzMjJlMzVQNC9aS1FNY0ZEdGNha3JkeWVrdVZUV2lLZjdCdmJybFpjMVhZQVo4V3pzPQ==;MjEzNDA2NUAzMjMxMmUzMjJlMzVRNEwyVEZaVnlvYnRhRkw2U1kwdnJXdmpaa1ZiYk5MNHI1ajhvWlI5RkpRPQ==;Mgo+DSMBMAY9C3t2VFhiQlRPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtRd0VqWH5ccnJXQ2k=;MjEzNDA2N0AzMjMxMmUzMjJlMzVoZ3dRb2tEbGIxQmQ3RldwL3cwZkJDVy94RlZyelhrMk5MTzBDMXA3ak9VPQ==;MjEzNDA2OEAzMjMxMmUzMjJlMzVkNk1iOW96YkV3YUlaL2NqWmFscEh0dHhkWEJURllGNFVEaTBZUXk5WmpRPQ==;MjEzNDA2OUAzMjMxMmUzMjJlMzVQNC9aS1FNY0ZEdGNha3JkeWVrdVZUV2lLZjdCdmJybFpjMVhZQVo4V3pzPQ==");


            DisplayRootViewForAsync<ShellViewModel>();

        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }


    }
}
