using EzWorkout.Models;
using EzWorkout.Services;
using EzWorkout.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EzWorkout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntervalPage : ContentPage
    {
        public IntervalPage(WorkoutViewModel workoutViewModel, IntervalType intervalType)
        {
            InitializeComponent();

            workout = workoutViewModel;

            switch (intervalType)
            {
                case IntervalType.DURATION:
                    DurationInterval();
                    break;
                case IntervalType.DISTANCE:
                    DistanceInterval();
                    break;
                case IntervalType.GOTO:
                    GoToInterval();
                    break;
                default:
                    throw new NotImplementedException("default");
            }
        }

        private void DurationInterval()
        {
            BindingContext = viewModel = new IntervalViewModel(new DurationInterval());

            DurationObj.IsVisible = true;
            IntensityObj.IsVisible = true;
        }
        private void DistanceInterval()
        {
            BindingContext = viewModel = new IntervalViewModel(new DistanceInterval());

            DistanceObj.IsVisible = true;
            IntensityObj.IsVisible = true;
        }
        private void GoToInterval()
        {
            BindingContext = viewModel = new IntervalViewModel(new GoToInterval());

            GoToObj.IsVisible = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }


        private IntervalViewModel viewModel;
        private WorkoutViewModel workout;
        private bool completed;

        //private async void BtnCancel(object sender, EventArgs e)
        //{
        //    await Navigation.PopAsync();
        //}
        //private async void BtnSave(object sender, EventArgs e)
        //{
        //    if (completed == true)
        //        workout.Intervals.Add(viewModel);

        //    await Navigation.PopAsync();
        //}
        private bool hasErrors()
        {
            if (
                viewModel.Intensity == 0 ||
                viewModel.Type == 0 ||
                viewModel.Duration == TimeSpan.Zero
                )
                return true;

            return false;
        }
    }
}