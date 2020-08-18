using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EzWorkout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            Btn_ResetDb.Clicked += BtnResetDb;
        }

        private async void BtnResetDb(object sender, EventArgs e)
        {
            if (!await DisplayAlert("Alert", "Are you sure?", "Yes", "No"))
                return;

            var allWorkouts = await App.Database.GetItemsAsync();

            foreach (var workout in allWorkouts)
            {
                await App.Database.DeleteItemAsync(workout);
            }

            await DisplayAlert("Alert", "Database cleared", "OK");
        }
    }
}