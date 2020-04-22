using System;
using System.Collections.Generic;
using System.Text;

namespace EzWorkout.Models
{
    public class Workout
    {
        public Workout()
        {
            Intervals = new List<Interval>();
        }
        public Workout(List<Interval> intervals)
        {
            Intervals = intervals;
        }

        public string Name { get; set; }
        public List<Interval> Intervals { get; set; }

    }
}
