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

            Types = new ObservableCollection<string>();
            var arr = Enum.GetValues(typeof(IntervalType));

            //Skip the first enum "NULL"
            for (int i = 1; i < arr.Length; i++)
            {
                Types.Add(arr.GetValue(i).ToString());
            }
            typeView.ItemsSource = Types;
            //if (intervalViewModel == null)
            //{
            //    newItem = true;
            //    //intervalViewModel = new IntervalViewModel(new _Interval());
            //}

            workout = workoutViewModel;
            //BindingContext = viewModel = intervalViewModel;
            BindingContext = this;

            //typePicker.SelectedIndexChanged += typeChanged;
            //timePicker.TimeSelected += timeChanged;

            //timePicker.IsVisible = viewModel.Type == IntervalType.DURATION ? true : false;
            //listView.SelectionChanged += SelectionChanged;
        }

        //private void SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        //{
        //    switch ((IntervalType)Enum.Parse(typeof(IntervalType), listView.SelectedItem.ToString()))
        //    {
        //        case (IntervalType.DISTANCE):
        //            break;
        //        case (IntervalType.DURATION):
        //            break;
        //        case (IntervalType.GOTO):
        //            break;
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public ObservableCollection<string> Types { get; private set; }

        private IntervalViewModel viewModel;
        private WorkoutViewModel workout;
        private bool newItem;

        private void typeChanged(object sender, EventArgs e)
        {
            //if (typePicker.SelectedItem == IntervalType.NULL)
            //{
            //    timePicker.IsVisible = false;
            //}
            //if (typePicker.SelectedItem == IntervalType.DISTANCE)
            //{
            //    timePicker.IsVisible = false;
            //}
            //if (typePicker.SelectedItem == IntervalType.DURATION)
            //{
            //    timePicker.IsVisible = true;
            //}
        }
        //private void timeChanged(object sender, EventArgs e)
        //{
        //    if (timePicker.Time > TimeSpan.Zero)
        //        viewModel.Duration = TimeSpan.FromSeconds(timePicker.Time.TotalSeconds);
        //}

        //private async void BtnContinue(object sender, EventArgs e)
        //{
        //    switch ((IntervalType)Enum.Parse(typeof(IntervalType), listView.SelectedItem.ToString()))
        //    {
        //        default:
        //            break;
        //    }
        //}
        private async void BtnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        //private async void BtnDuration(object sender, EventArgs e)
        //{
        //    await Navigation.PopAsync();
        //}
        //private async void BtnDistance(object sender, EventArgs e)
        //{
        //    await Navigation.PopAsync();
        //}
        //private async void BtnGoTo(object sender, EventArgs e)
        //{
        //    await Navigation.PopAsync();
        //}
        //private async void BtnSave(object sender, EventArgs e)
        //{
        //    if (hasErrors())
        //        return;

        //    if (newItem == true)
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