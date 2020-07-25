using EzWorkout.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
        private GpsManager _gpsManager;
        private CancellationTokenSource _cts;

        public bool TrackDistance;

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
                Start();
            }
            else
            {
                Stop();
            }
        }

        public void Start()
        {
            _stopWatch.Start();
            _cts = new CancellationTokenSource();

            ButtonText_StartStop = "STOP";

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                Duration = _stopWatch.Elapsed.ToString(@"hh\:mm\:ss\.fff");

                if (TrackDistance == true)
                {
                    _gpsManager.TrackDistance(_cts);
                    Distance = Humanizer.DistanceFromKm(_gpsManager.TotalDistance);
                }

                if (_stopWatch.IsRunning == false)
                    return false;
                else
                    return true;
            });
        }
        public void Stop()
        {
            if (_cts != null)
                _cts.Cancel();

            _stopWatch.Stop();
            ButtonText_StartStop = "RESUME";
        }
        public void Reset()
        {
            if (_cts != null)
                _cts.Cancel();

            Duration = "00:00:00.000";
            Distance = "0 m";
            ButtonText_StartStop = "START";

            _stopWatch = new Stopwatch();
            _gpsManager = new GpsManager();
            _cts = new CancellationTokenSource();
        }
    }
}
