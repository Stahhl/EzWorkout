using EzWorkout.ViewModels;
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
    public partial class StopwatchPage : ContentPage
    {
        public StopwatchPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new StopwatchViewModel();

            Switch_ToggleDistance.Toggled += ToggleDistance;
            Button_Start.Clicked += StartStop;
            Button_Reset.Clicked += Reset;
        }

        private StopwatchViewModel viewModel;

        private void Reset(object sender, EventArgs e)
        {
            viewModel.Reset();
        }

        private void StartStop(object sender, EventArgs e)
        {
            viewModel.StartStop();
        }

        private void ToggleDistance(object sender, ToggledEventArgs e)
        {
            Stack_Distance.IsVisible = Switch_ToggleDistance.IsToggled;
        }
    }
}