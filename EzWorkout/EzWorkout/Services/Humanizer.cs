using System;
using System.Collections.Generic;
using System.Text;

namespace EzWorkout.Services
{
    public static class Humanizer
    {
        public static string DistanceFromKm(double km)
        {
            if (km < 1)
                return km * 1000 + " m";

            return km + " km";
        }
    }
}
