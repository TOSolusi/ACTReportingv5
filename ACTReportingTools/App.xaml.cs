using System.Configuration;
using System.Data;
using System.Windows;

namespace ACTReportingTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //register syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmtCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX5fcHRTR2JYUEd+W0c=");
        }
    }

}
