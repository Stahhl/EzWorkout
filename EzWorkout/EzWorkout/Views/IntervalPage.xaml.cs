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

            Btn1.Clicked += Btn_Intensity;
            Btn2.Clicked += Btn_Intensity;
            Btn3.Clicked += Btn_Intensity;
            Btn4.Clicked += Btn_Intensity;

            BtnCancel.Clicked += Btn_Cancel;
            BtnSave.Clicked += Btn_Save;
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

        private IntervalViewModel viewModel;
        private WorkoutViewModel workout;
        private bool newItem;

        private void Btn_Intensity(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandParameter.ToString());
            _intensity = (IntervalIntensity)id;

            IntensityPicker(id);
        }
        private void Btn_Cancel(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void Btn_Save(object sender, EventArgs e)
        {
            if (UpdateInterval() == false)
                return;
            if (newItem == true)
                workout.Intervals.Add(viewModel);

            Navigation.PopAsync();
        }

        private IntervalIntensity _intensity;

        private void IntensityPicker(int id)
        {
            Color inactiveColor = Color.LightGray;
            Color activeColor = Color.LightBlue;

            Btn1.BackgroundColor = inactiveColor;
            Btn2.BackgroundColor = inactiveColor;
            Btn3.BackgroundColor = inactiveColor;
            Btn4.BackgroundColor = inactiveColor;

            switch (id)
            {
                case 1:
                    Btn1.BackgroundColor = activeColor;
                    break;
                case 2:
                    Btn2.BackgroundColor = activeColor;
                    break;
                case 3:
                    Btn3.BackgroundColor = activeColor;
                    break;
                case 4:
                    Btn4.BackgroundColor = activeColor;
                    break;
                default:
                    throw new Exception("default");
            }
        }

        private void DurationInterval(IntervalViewModel intervalViewModel = null)
        {
            if (intervalViewModel == null)
                BindingContext = viewModel = new IntervalViewModel(new DurationInterval());
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
            if(intervalViewModel == null)
                BindingContext = viewModel = new IntervalViewModel(new DistanceInterval());
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
            //BindingContext = viewModel = new IntervalViewModel(new GoToInterval());

            //GoToObj.IsVisible = true;
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
                    viewModel.Distance = dist;

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
                    viewModel.Duration = timePicker.Time;

                    return true;
                }
            }
            if (viewModel.Type == IntervalType.GOTO)
            {

            }

            return false;
        }
    }
}