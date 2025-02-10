using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;
using ACTReportingTools.Models;

namespace ACTReportingTools.Helpers
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (value as DisplayRecordModel).status;
            //custom condition is checked based on data.
            if (input == 2)
                return new SolidColorBrush(Colors.Bisque);
            //else if (input < 1007)
               // return new SolidColorBrush(Colors.LightBlue);
            else
                return DependencyProperty.UnsetValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
  
}
