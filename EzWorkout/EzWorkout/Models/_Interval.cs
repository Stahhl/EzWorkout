using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EzWorkout.Services;
using SQLite;
using Xamarin.Forms;

namespace EzWorkout.Models
{
    public class _Interval
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int WorkoutId { get; set; }

        //Enums
        public IntervalIntensity Intensity { get; set; }
        public IntervalType Type { get; set; }

        //Duration
        public TimeSpan Duration { get; set; }
        public TimeSpan CurrentDuration { get; set; }

        //Distance
        public int Distance { get; set; }
        public int CurrentDistance { get; set; }

        //GoTo
        public int GoTo { get; set; }
        public int Repeat { get; set; }
    }
}
