﻿using EzWorkout.Models;
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

        private void BtnIntensity(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandParameter.ToString());

            viewModel.Intensity = (IntervalIntensity)id;

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
                    break;
            }
        }
        private IntervalViewModel viewModel;
        private WorkoutViewModel workout;

        private void DurationInterval()
        {
            BindingContext = viewModel = new IntervalViewModel(new DurationInterval());

            DurationObj.IsVisible = true;
            IntensityObj.IsVisible = true;
        }
        private void DistanceInterval()
        {
            //BindingContext = viewModel = new IntervalViewModel(new DistanceInterval());

            //DistanceObj.IsVisible = true;
            //IntensityObj.IsVisible = true;
        }
        private void GoToInterval()
        {
            //BindingContext = viewModel = new IntervalViewModel(new GoToInterval());

            //GoToObj.IsVisible = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }



        //private bool hasErrors()
        //{
        //    if (
        //        viewModel.Intensity == 0 ||
        //        viewModel.Type == 0 ||
        //        viewModel.Duration == TimeSpan.Zero
        //        )
        //        return true;

        //    return false;
        //}
    }
}