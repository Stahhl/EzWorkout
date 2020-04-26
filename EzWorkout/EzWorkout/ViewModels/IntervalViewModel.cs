using EzWorkout.Models;
using EzWorkout.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xamarin.Forms;

namespace EzWorkout.ViewModels
{
    public class IntervalViewModel : BaseViewModel
    {
        public IntervalViewModel(Interval _interval)
        {
            interval = _interval;
            Color = Color.LightBlue;
        }

        public Interval interval;
        private Color color;
        private bool isSelected;

        public Color Color
        {
            get { return color; }
            set { SetProperty(ref color, value); }
        }
        public IntervalIntensity Intensity
        {
            get { return interval.Intensity; }
            set { SetProperty(ref interval.Intensity, value); }
        }
        public IntervalType Type
        {
            get { return interval.Type; }
            set { SetProperty(ref interval.Type, value); }
        }
        public int Amount
        {
            get { return interval.Amount; }
            set { SetProperty(ref interval.Amount, value); }
        }

        public void ToggleSelection()
        {
            isSelected = !isSelected;

            if (isSelected == true)
            {
                Color = Color.Green;
            }
            else
            {
                Color = Color.LightBlue;
            }
        }
    }
}
