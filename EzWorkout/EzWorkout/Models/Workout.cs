using SQLite;
using System.Collections.Generic;

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

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        [Ignore]
        public List<_Interval> Intervals { get; set; }

    }
}
