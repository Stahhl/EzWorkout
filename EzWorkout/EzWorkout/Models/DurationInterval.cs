using EzWorkout.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzWorkout.Models
{
    public class DurationInterval : _Interval
    {
        public DurationInterval()
        {
            Type = IntervalType.DURATION;
        }
        public DurationInterval(IntervalIntensity intensity, TimeSpan duration)
        {
            Type = IntervalType.DURATION;
            Intensity = intensity;

            Duration = duration;
            CurrentDuration = duration;
        }
    }
}
