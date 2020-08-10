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
        //Enums
        public IntervalIntensity Intensity;
        public IntervalType Type;

        //Duration
        public TimeSpan Duration;
        public TimeSpan CurrentDuration;

        //Distance
        public int Distance;
        public int CurrentDistance;

        //GoTo
        public int GoTo;
        public int Repeat;
    }
}
