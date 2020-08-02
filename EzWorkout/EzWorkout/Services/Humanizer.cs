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
                return Math.Floor(km * 1000) + " m";

            return km.ToString("0.00") + " km";
        }

        public static string DistanceFromM(int meters)
        {
            int m = meters > 1000 ? meters % 1000 : meters;
            int km = meters > 1000 ? (meters - m) / 1000 : 0;

            string result = "";

            result += meters <= 0 ? "0m " : "";
            result += km > 0 ? $"{km}km " : "";
            result += m > 0 ? $"{m}m " : "";

            return result;
        }
    }
}
