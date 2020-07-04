using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EzWorkout.Services;
using EzWorkout.Views;

namespace EzWorkout
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //app needs a "appsettings.json" in root
            //"appsettings.json" is in gitignore
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(AppSettingsManager.Settings["SyncFusionLicense"]);

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
