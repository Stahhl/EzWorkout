using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EzWorkout.Models;
using EzWorkout.Services;
using System.Linq;

namespace EzWorkout.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            //Start page set in xaml

            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            //MenuPages.Add((int)AppPage.Home, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            ContentPage newPage = null;

            switch (id)
            {
                case (int)AppPage.Home:
                    newPage = new HomePage();
                    break;
                case (int)AppPage.Workouts:
                    newPage = new BrowseWorkoutsPage();
                    break;
                case (int)AppPage.Stopwatch:
                    newPage = new StopwatchPage();
                    break;
                case (int)AppPage.About:
                    newPage = new AboutPage();
                    break;
                default:
                    throw new Exception("Page doesn't exist!");
            }

            var newNavPage = new NavigationPage(newPage);

            if (newNavPage != null && Detail != newNavPage)
            {
                Detail = newNavPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}