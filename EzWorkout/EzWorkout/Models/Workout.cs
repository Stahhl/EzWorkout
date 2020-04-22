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

        public string Name { get; set; }
        public List<Interval> Intervals { get; set; }

    }
}
