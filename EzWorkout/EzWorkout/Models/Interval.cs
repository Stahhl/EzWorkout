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
        public IntervalIntensity Intensity;
        public IntervalType Type;
        public int Amount;
    }
}
