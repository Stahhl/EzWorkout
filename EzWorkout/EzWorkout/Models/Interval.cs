using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EzWorkout.Services;
using Xamarin.Forms;

namespace EzWorkout.Models
{
    public class Interval
    {
        public IntervalIntensity Intensity { get; set; }
        public IntervalType Type { get; set; }
        public int Amount { get; set; } //100m or 1s
    }
}
