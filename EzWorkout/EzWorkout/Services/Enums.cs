using System;
using System.Collections.Generic;
using System.Text;

namespace EzWorkout.Services
{
    public enum IntervalIntensity
    {
        NULL,
        INACTIVE,
        LIGHT,
        MEDIUM,
        HIGH,
    }
    public enum IntervalType
    {
        NULL,
        DURATION,
        DISTANCE,
        GOTO
    }
    public enum AppPage
    {
        Home,
        Workouts,
        Stopwatch,
        About
    }

}
