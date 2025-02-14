using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ACTReportingTools.Helpers
{
    public class PAsswordBoxHelper
    {
        public static class PasswordBoxHelper
        {
            public static readonly DependencyProperty BoundPasswordProperty =
                DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxHelper),
                    new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

            public static string GetBoundPassword(DependencyObject d)
            {
                return (string)d.GetValue(BoundPasswordProperty);
            }

            public static void SetBoundPassword(DependencyObject d, string value)
            {
                d.SetValue(BoundPasswordProperty, value);
            }

            private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                if (d is PasswordBox passwordBox)
                {
                    passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

                    if (!(bool)passwordBox.GetValue(IsUpdatingProperty))
                    {
                        passwordBox.Password = (string)e.NewValue;
                    }

                    passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
                }
            }

            private static readonly DependencyProperty IsUpdatingProperty =
                DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordBoxHelper));

            private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
            {
                if (sender is PasswordBox passwordBox)
                {
                    passwordBox.SetValue(IsUpdatingProperty, true);
                    SetBoundPassword(passwordBox, passwordBox.Password);
                    passwordBox.SetValue(IsUpdatingProperty, false);
                }
            }
        }

    }
}
