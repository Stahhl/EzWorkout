using System;
using System.Collections.Generic;
using System.Text;

namespace EzWorkout.Models
{
    public class Workout
    {
        public Workout()
        {
            Intervals = new List<_Interval>();
        }
        public Workout(List<_Interval> intervals)
        {
            Intervals = intervals;
        }

        public string Name { get; set; }
        public List<_Interval> Intervals { get; set; }

    }
}
