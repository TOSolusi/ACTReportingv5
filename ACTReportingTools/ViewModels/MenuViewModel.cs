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
    public class MenuViewModel : Screen
    {
        
        ShellViewModel contentModel = IoC.Get<ShellViewModel>();
        public void Button1()
        {
            // Add login logic here
            // For simplicity, let's just display a message for now
            
            contentModel.MainContent = IoC.Get<ContentViewModel>();
            //contentModel = IoC.Get<ContentViewModel>();
            //await ActivateItemAsync(contentModel, new CancellationToken());
            //MainContent = contentModel.MainContent;
            //MessageBox.Show($"Logged In Successfully!\nButton1");
        }

        public async Task Button2()
        {
            // Add login logic here
            // For simplicity, let's just display a message for now
            //var contentModel = IoC.Get<Content1ViewModel>();
            //await ActivateItemAsync(contentModel, new CancellationToken());
            //MainContent = contentModel;
            //await contentModel.LoadContent1();
            contentModel.MainContent = IoC.Get<Content1ViewModel>();
            //MessageBox.Show($"Logged In Successfully!\nButton2");
        }

        public async Task Button3()
        {
            // Add login logic here
            // For simplicity, let's just display a message for now
            //var contentModel = IoC.Get<Content2ViewModel>();
            //await ActivateItemAsync(contentModel, new CancellationToken());
            //MainContent = contentModel;
            //await contentModel.LoadContent2();
            //contentModel.Refresh();
            //MessageBox.Show($"Logged In Successfully!\nButton3");
            contentModel.MainContent = IoC.Get<Content2ViewModel>();
        }
    }
}
