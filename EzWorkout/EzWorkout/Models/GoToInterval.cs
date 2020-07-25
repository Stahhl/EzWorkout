﻿using EzWorkout.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzWorkout.Models
{
    public class GoToInterval : _Interval
    {
        public GoToInterval()
        {
        }

        public GoToInterval(int intervalNumber)
        {
            Type = IntervalType.GOTO;
            IntervalNumber = intervalNumber;
        }
    }
}