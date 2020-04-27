using EzWorkout.Models;
using EzWorkout.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
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
        public TimeSpan Duration
        {
            get { return interval.Duration; }
            set { SetProperty(ref interval.Duration, value); }
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
        public async Task Countdown()
        {
            int ms = 1000;

            while (Duration.TotalSeconds > 0)
            {
                await Task.Delay(ms);

                Duration = Duration.Subtract(TimeSpan.FromMilliseconds(ms));
            }
        }
    }
}
