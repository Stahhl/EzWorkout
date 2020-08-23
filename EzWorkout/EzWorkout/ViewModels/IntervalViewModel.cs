using EzWorkout.Models;
using EzWorkout.Services;
using EzWorkout.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EzWorkout.ViewModels
{
    public class IntervalViewModel : _BaseViewModel
    {
        public IntervalViewModel(_Interval interval, int intervalIndex)
        {
            _interval = interval;
            IntervalIndex = intervalIndex;

            Reset();

            Color = Color.LightBlue;

            Types = new ObservableCollection<string>();
            var arr = Enum.GetValues(typeof(IntervalType));

            //Skip the first enum "NULL"
            for (int i = 1; i < arr.Length; i++)
            {
                Types.Add(arr.GetValue(i).ToString());
            }
        }

        private _Interval _interval;
        private Color _color;
        private bool _isSelected;
        private int _intervalIndex;
        private TimeSpan _duration;
        private int _distance;
        private int _repeat;

        public ObservableCollection<string> Types { get; private set; }

        public _Interval Interval
        {
            get { return _interval; }
            private set
            {
                _interval = value;
            }
        }
        public int IntervalIndex
        {
            get { return _intervalIndex; }
            set
            {
                _intervalIndex = value;

                OnPropertyChanged(nameof(IntervalIndex));
            }
        }
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;

                OnPropertyChanged(nameof(Color));
            }
        }
        public IntervalIntensity Intensity
        {
            get { return _interval.Intensity; }
            set
            {
                Interval.Intensity = value;

                OnPropertyChanged(nameof(Intensity));
            }
        }
        public IntervalType Type
        {
            get { return _interval.Type; }
            set
            {
                _interval.Type = value;

                OnPropertyChanged(nameof(Type));
            }
        }
        public int GoTo
        {
            get { return _interval.GoTo; }
            set
            {
                if (value < 0 || value >= IntervalIndex)
                    return;

                _interval.GoTo = value;

                OnPropertyChanged(nameof(GoToString));
                OnPropertyChanged(nameof(_interval.GoTo));
            }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;

                OnPropertyChanged(nameof(Duration));
            }
        }
        public TimeSpan DurationMax
        {
            get { return _interval.Duration; }
            set
            {
                _interval.Duration = value;
                Duration = value;

                OnPropertyChanged(nameof(DurationMax));
            }
        }

        public int Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;

                OnPropertyChanged(nameof(DistanceString));
                OnPropertyChanged(nameof(Distance));
            }
        }
        public int DistanceMax
        {
            get { return _interval.Distance; }
            set
            {
                _interval.Distance = value;
                Distance = value;

                OnPropertyChanged(nameof(DistanceMax));
            }
        }

        public int Repeat
        {
            get { return _repeat; }
            set
            {
                if (value < 0)
                    return;

                _repeat = value;

                OnPropertyChanged(nameof(RepeatString));
            }
        }
        public int RepeatMax
        {
            get { return _interval.Repeat; }
            set
            {
                if (value < 0)
                    return;

                _interval.Repeat = value;
                Repeat = _interval.Repeat;

                OnPropertyChanged(nameof(RepeatMax));
            }
        }

        public string GoToString
        {
            get
            {
                return $"Go to: {GoTo}";
            }
        }
        public string RepeatString
        {
            get
            {
                return $"Repeat: {Repeat}";
            }
        }
        public string DistanceString
        {
            //OnPropertyChanged from setter on this.Distance
            get { return Humanizer.DistanceFromM(Distance); }
        }

        public bool GoToEnabled
        {
            get
            {
                return Type == IntervalType.GOTO ? true : false;
            }
        }
        public bool DurationEnabled
        {
            get
            {
                return Type == IntervalType.DURATION ? true : false;
            }
        }
        public bool DistanceEnabled
        {
            get
            {
                return Type == IntervalType.DISTANCE ? true : false;
            }
        }

        public void Reset()
        {
            _isSelected = false;
            Duration = DurationMax;
            Distance = DistanceMax;
            Repeat = RepeatMax;
            Color = Color.LightBlue;
        }

        public void ToggleSelection()
        {
            _isSelected = !_isSelected;

            if (_isSelected == true)
            {
                Color = Color.Green;
            }
            else
            {
                Color = Color.LightBlue;
            }
        }
        public void CountdownTime(CancellationTokenSource cts)
        {
            int tick = 10;

            int current = 0;
            int max = 1000;

            while (Duration.TotalSeconds > 0)
            {
                if (cts.IsCancellationRequested)
                    throw new TaskCanceledException();

                current += tick;

                if (current >= max)
                {
                    Duration -= TimeSpan.FromMilliseconds(max);
                    current = 0;
                }

                //await Task.Delay(tick);
                Thread.Sleep(tick);
            }
        }
        public void CountdownDistance(CancellationTokenSource cts)
        {
            var gpsManager = new GpsManager();
            gpsManager.TrackDistance(cts);

            while (Distance > 0)
            {
                double dist = gpsManager.TotalDistance;

                if (dist > 0.001)
                {
                    double m = dist * 1000;
                    double floor = Math.Floor(m);
                    double km = floor * 0.001;

                    gpsManager.TotalDistance -= km;
                    Distance -= (int)floor;
                }
            }
        }
        public void CountdownGoTo()
        {
            if (Repeat == 0)
                return;

            Repeat--;
        }

    }
}
