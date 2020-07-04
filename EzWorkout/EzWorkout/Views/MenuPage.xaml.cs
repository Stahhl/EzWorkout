using EzWorkout.Models;
using EzWorkout.Services;
using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EzWorkout.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        private MainPage _rootPage { get => Application.Current.MainPage as MainPage; }
        private List<AppPage> _menuItems;

        public MenuPage()
        {
            InitializeComponent();

            _menuItems = Enum.GetValues(typeof(AppPage)).OfType<AppPage>().ToList();

            ListViewMenu.ItemsSource = _menuItems;

            ListViewMenu.SelectedItem = _menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((AppPage)e.SelectedItem);
                await _rootPage.NavigateFromMenu(id);
            };
        }
    }
}