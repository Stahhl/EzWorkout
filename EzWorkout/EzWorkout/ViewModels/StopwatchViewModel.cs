using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace EzWorkout.ViewModels
{
    public class StopwatchViewModel : _BaseViewModel
    {
        public StopwatchViewModel()
        {
            Reset();
        }

        private Stopwatch _stopWatch;

        private string _duration;
        private string _distance;
        private string _buttonText_StartStop;

        public string Duration
        {
            get { return _duration; }
            set { SetProperty(ref _duration, value); }
        }
        public string Distance
        {
            get { return _distance; }
            set { SetProperty(ref _distance, value); }
        }
        public string ButtonText_StartStop
        {
            get { return _buttonText_StartStop; }
            set { SetProperty(ref _buttonText_StartStop, value); }
        }

        public void StartStop()
        {
            if (_stopWatch.IsRunning == false)
            {
                _stopWatch.Start();

                ButtonText_StartStop = "STOP";

                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    Duration = _stopWatch.Elapsed.ToString(@"hh\:mm\:ss\.fff");

                    if (_stopWatch.IsRunning == false)
                        return false;
                    else
                        return true;
                });
            }
            else
            {
                _stopWatch.Stop();

                ButtonText_StartStop = "START";
            }
        }
        public void Reset()
        {
            Duration = "00:00:00.000";
            Distance = "0 m";
            ButtonText_StartStop = "START";

            _stopWatch = new Stopwatch();
        }

    }
}
