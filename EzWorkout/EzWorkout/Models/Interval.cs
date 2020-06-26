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
        public Interval()
        {

        }
        public Interval(IntervalIntensity intensity, TimeSpan duration)
        {
            Type = IntervalType.DURATION;
            Intensity = intensity;

            Duration = duration;
            CurrentDuration = duration;
        }
        public Interval(IntervalIntensity intensity, int distance)
        {
            Type = IntervalType.DISTANCE;
            Intensity = intensity;

            Distance = distance;
            CurrentDistance = distance;
        }

        public IntervalIntensity Intensity;
        public IntervalType Type;

        public TimeSpan Duration;
        public TimeSpan CurrentDuration;

        public int Distance;
        public int CurrentDistance;
    }
}
