﻿using System.Windows;
using Controlers;

namespace School_scheduler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new MainControler();
        }
    }
}
