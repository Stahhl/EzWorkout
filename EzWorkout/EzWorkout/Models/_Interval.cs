using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EzWorkout.Services;
using Xamarin.Forms;

namespace EzWorkout.Models
{
    public abstract class _Interval
    {
        public IntervalIntensity Intensity;
        public IntervalType Type;

        public TimeSpan Duration;
        public TimeSpan CurrentDuration;

        public int Distance;
        public int CurrentDistance;
        public int IntervalNumber;
    }
}
