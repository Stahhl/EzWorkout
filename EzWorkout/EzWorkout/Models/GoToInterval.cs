using EzWorkout.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzWorkout.Models
{
    public class GoToInterval : _Interval
    {
        public GoToInterval()
        {
            Type = IntervalType.GOTO;
        }

        public GoToInterval(int goTo, int repeat)
        {
            Type = IntervalType.GOTO;
            GoTo = goTo;
            Repeat = repeat;
        }
    }
}
