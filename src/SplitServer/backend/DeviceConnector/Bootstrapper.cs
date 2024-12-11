namespace DeviceConnector
{
    using Caliburn.Micro;
    using DeviceConnector.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    internal class Bootstrapper:BootstrapperBase
    {
        public Bootstrapper()
        {
            this.Initialize();
            LogManager.GetLog = type => new DebugLog(type);

        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            //base.OnStartup(sender, e);
            await this.DisplayRootViewForAsync(typeof(SujingViewModel));
        }
    }
}
