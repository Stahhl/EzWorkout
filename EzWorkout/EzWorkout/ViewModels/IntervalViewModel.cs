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

            Intensity = (IntervalIntensity)_interval.Intensity;
            Type = (IntervalType)_interval.Type;
            Amount = _interval.Amount;

            MyColor = Color.LightBlue;
        }

        private Interval interval;
        private Color myColor;

        [Required]
        public IntervalIntensity Intensity { get; set; }
        [Required]
        public IntervalType Type { get; set; }
        [Required]
        public int Amount { get; set; } //100m or 1s

        public Color MyColor
        {
            get { return myColor; }
            set { SetProperty(ref myColor, value); }
        }
    }
}
