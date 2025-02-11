using Microsoft.DotNet.DesignTools.Protocol.Values;
using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ACTReportingTools.Helpers
{
    public class CaptionSummaryRowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value != null ? value as Group : null;
            if (data != null)
            {
                SfDataGrid dataGrid = (SfDataGrid)parameter;
                //var unitPrice = SummaryCreator.GetSummaryDisplayText(data.SummaryDetails, "UnitPrice", dataGrid.View);
                //var count = SummaryCreator.GetSummaryDisplayText(data.SummaryDetails, "ProductName", dataGrid.View);

                //return "Total Price : " + unitPrice.ToString() + " for " + count.ToString() + " Products ";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    
    
}
