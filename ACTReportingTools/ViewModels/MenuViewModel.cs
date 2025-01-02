using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ACTReportingTools.ViewModels
{
    public class MenuViewModel() : Screen
    {
        
        ShellViewModel contentModel = IoC.Get<ShellViewModel>();
        

        public void Button1()
        {
            // Add login logic here
            // For simplicity, let's just display a message for now
            
            contentModel.MainContent = IoC.Get<ContentViewModel>();
            
        }

        public async Task Button2()
        {
            // Add login logic here
            // For simplicity, let's just display a message for now
            contentModel.MainContent = IoC.Get<Content1ViewModel>();
        }

        public async Task Button3()
        {
            // Add login logic here
            // For simplicity, let's just display a message for now
            contentModel.MainContent = IoC.Get<Content2ViewModel>();
        }
    }
}
