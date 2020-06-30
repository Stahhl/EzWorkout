using EzWorkout.Models;
using EzWorkout.Services;
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
    public partial class IntervalPage : ContentPage
    {
        public IntervalPage(IntervalViewModel intervalViewModel, WorkoutViewModel workoutViewModel)
        {
            InitializeComponent();

            if(intervalViewModel == null)
            {
                newItem = true;
                intervalViewModel = new IntervalViewModel(new Interval());
            }

            workout = workoutViewModel;
            BindingContext = viewModel = intervalViewModel;

            typePicker.SelectedIndexChanged += typeChanged;
            timePicker.TimeSelected += timeChanged;

            timePicker.IsVisible = viewModel.Type == IntervalType.DURATION ? true : false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private IntervalViewModel viewModel;
        private WorkoutViewModel workout;
        private bool newItem;

        private void typeChanged(object sender, EventArgs e)
        {
            //if (typePicker.SelectedItem == IntervalType.NULL)
            //{
            //    timePicker.IsVisible = false;
            //}
            if (typePicker.SelectedItem == IntervalType.DISTANCE)
            {
                timePicker.IsVisible = false;
            }
            if (typePicker.SelectedItem == IntervalType.DURATION)
            {
                timePicker.IsVisible = true;
            }
        }
        private void timeChanged(object sender, EventArgs e)
        {
            if (timePicker.Time > TimeSpan.Zero)
                viewModel.Duration = TimeSpan.FromSeconds(timePicker.Time.TotalSeconds);
        }


        private async void BtnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void BtnSave(object sender, EventArgs e)
        {
            if (hasErrors())
                return;

            if (newItem == true)
                workout.Intervals.Add(viewModel);

            await Navigation.PopAsync();
        }
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