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
        }

        private StopwatchViewModel viewModel;

        private void ToggleDistance(object sender, ToggledEventArgs e)
        {
            Stack_Distance.IsVisible = Switch_ToggleDistance.IsToggled;
        }
    }
}