using System;
using System.Collections.Generic;
using System.Text;

namespace EzWorkout.ViewModels
{
    public class StopwatchViewModel : _BaseViewModel
    {
        public StopwatchViewModel()
        {
            Duration = "00:00:00.000";
            Distance = "0 m";
        }

        private string _duration;
        private string _distance;
        private string _buttonText_StartStop;

        public string Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        public string Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }
        public string ButtonText_StartStop
        {
            get { return _buttonText_StartStop; }
            set { _buttonText_StartStop = value; }
        }

    }
}
