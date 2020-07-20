using EzWorkout.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzWorkout.Models
{
    public class DistanceInterval : _Interval
    {
        public DistanceInterval()
        {
        }

        public DistanceInterval(IntervalIntensity intensity, int distance)
        {
            Type = IntervalType.DISTANCE;
            Intensity = intensity;

            Distance = distance;
            CurrentDistance = distance;
        }
    }
}
