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
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EzWorkout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntervalPage : ContentPage
    {
        private void Init(WorkoutViewModel workoutViewModel)
        {
            InitializeComponent();

            workout = workoutViewModel;

            btn1.Clicked += BtnIntensity;
            btn2.Clicked += BtnIntensity;
            btn3.Clicked += BtnIntensity;
            btn4.Clicked += BtnIntensity;

            btnGoToMinus.Clicked += BtnGoTo;
            btnGoToPlus.Clicked += BtnGoTo;
            btnRepeatMinus.Clicked += BtnRepeat;
            btnRepeatPlus.Clicked += BtnRepeat;

            btnCancel.Clicked += BtnCancel;
            btnSave.Clicked += BtnSave;
        }
        /// <summary>
        /// Edit an existing interval
        /// </summary>
        /// <param name="workoutViewModel"></param>
        /// <param name="intervalViewModel"></param>
        public IntervalPage(WorkoutViewModel workoutViewModel, IntervalViewModel intervalViewModel)
        {
            Init(workoutViewModel);

            newItem = false;

            _intensity = intervalViewModel.Intensity;

            switch (intervalViewModel.Type)
            {
                case IntervalType.DURATION:
                    DurationInterval(intervalViewModel);
                    break;
                case IntervalType.DISTANCE:
                    DistanceInterval(intervalViewModel);
                    break;
                case IntervalType.GOTO:
                    GoToInterval(intervalViewModel);
                    break;
                default:
                    throw new NotImplementedException("default");
            }
        }
        /// <summary>
        /// Create a new interval of param type
        /// </summary>
        /// <param name="workoutViewModel"></param>
        /// <param name="intervalType"></param>
        public IntervalPage(WorkoutViewModel workoutViewModel, IntervalType intervalType)
        {
            Init(workoutViewModel);

            newItem = true;

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

        private IntervalIntensity _intensity;
        private IntervalViewModel viewModel;
        private WorkoutViewModel workout;
        private bool newItem; //constructor

        #region controls
        private void BtnIntensity(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandParameter.ToString());
            _intensity = (IntervalIntensity)id;

            IntensityPicker(id);
        }
        private void BtnCancel(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private async void BtnSave(object sender, EventArgs e)
        {
            if (UpdateInterval() == false)
                return;

            if (newItem == true)
            {
                workout.Intervals.Add(viewModel);
            }

            await App.Database.SaveItemAsync(viewModel.Interval, workout.Workout);
            await Navigation.PopAsync();
        }
        private void BtnGoTo(object sender, EventArgs e)
        {
            int change = int.Parse(((Button)sender).CommandParameter.ToString());

            viewModel.GoTo += change;
        }
        private void BtnRepeat(object sender, EventArgs e)
        {
            int change = int.Parse(((Button)sender).CommandParameter.ToString());

            viewModel.Repeat += change;
        }
        #endregion

        private void IntensityPicker(int id)
        {
            Color inactiveColor = Color.LightGray;
            Color activeColor = Color.LightBlue;

            btn1.BackgroundColor = inactiveColor;
            btn2.BackgroundColor = inactiveColor;
            btn3.BackgroundColor = inactiveColor;
            btn4.BackgroundColor = inactiveColor;

            switch (id)
            {
                case 1:
                    btn1.BackgroundColor = activeColor;
                    break;
                case 2:
                    btn2.BackgroundColor = activeColor;
                    break;
                case 3:
                    btn3.BackgroundColor = activeColor;
                    break;
                case 4:
                    btn4.BackgroundColor = activeColor;
                    break;
                default:
                    throw new Exception("default");
            }
        }
        private void DurationInterval(IntervalViewModel intervalViewModel = null)
        {
            if (intervalViewModel == null)
                BindingContext = viewModel = new IntervalViewModel(new DurationInterval(), workout.Intervals.Count);
            else
            {
                BindingContext = viewModel = intervalViewModel;
                IntensityPicker((int)intervalViewModel.Intensity);
            }

            IntensityObj.IsVisible = true;
            DurationObj.IsVisible = true;
        }
        private void DistanceInterval(IntervalViewModel intervalViewModel = null)
        {
            if (intervalViewModel == null)
                BindingContext = viewModel = new IntervalViewModel(new DistanceInterval(), workout.Intervals.Count);
            else
            {
                BindingContext = viewModel = intervalViewModel;
                IntensityPicker((int)intervalViewModel.Intensity);
            }

            IntensityObj.IsVisible = true;
            DistanceObj.IsVisible = true;
        }
        private void GoToInterval(IntervalViewModel intervalViewModel = null)
        {
            if (intervalViewModel == null)
                BindingContext = viewModel = new IntervalViewModel(new GoToInterval(), workout.Intervals.Count);
            else
            {
                BindingContext = viewModel = intervalViewModel;
            }

            GoToObj.IsVisible = true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private bool UpdateInterval()
        {
            if (viewModel.Type == IntervalType.DISTANCE)
            {
                int dist = int.Parse(DistanceEntry.Text);

                if (_intensity == IntervalIntensity.NULL || dist <= 0)
                    return false;
                else
                {
                    viewModel.Intensity = _intensity;
                    viewModel.DistanceMax = dist;

                    return true;
                }
            }
            if (viewModel.Type == IntervalType.DURATION)
            {
                if (_intensity == IntervalIntensity.NULL || timePicker.Time <= TimeSpan.Zero)
                    return false;
                else
                {
                    viewModel.Intensity = _intensity;
                    viewModel.DurationMax = timePicker.Time;

                    return true;
                }
            }
            if (viewModel.Type == IntervalType.GOTO)
            {
                if ((viewModel.GoTo < 0 || viewModel.GoTo > workout.Intervals.Count) || viewModel.Repeat < 0)
                    return false;
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}