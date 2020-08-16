﻿using EzWorkout.Models;
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
                OnPropertyChanged(nameof(IntervalIndex));

                _intervalIndex = value;
            }
        }
        public Color Color
        {
            get { return _color; }
            set 
            {
                OnPropertyChanged(nameof(Color));

                _color = value;
            }
        }
        public IntervalIntensity Intensity
        {
            get { return _interval.Intensity; }
            set 
            {
                OnPropertyChanged(nameof(Intensity));

                Interval.Intensity = value;
            }
        }
        public IntervalType Type
        {
            get { return _interval.Type; }
            set 
            {
                OnPropertyChanged(nameof(Type));

                _interval.Type = value;
            }
        }
        public TimeSpan Duration
        {
            get { return _interval.CurrentDuration; }
            set 
            {
                OnPropertyChanged(nameof(Duration));

                _interval.CurrentDuration = value;
            }
        }
        public int Distance
        {
            get { return _interval.CurrentDistance; }
            set
            {
                OnPropertyChanged(nameof(DistanceHumanized));
                OnPropertyChanged(nameof(Distance));

                _interval.CurrentDistance = value;
            }
        }
        public string DistanceHumanized
        {
            //OnPropertyChanged from setter on this.Distance
            get { return Humanizer.DistanceFromM(Distance); }
        }
        public int GoTo
        {
            get { return _interval.GoTo; }
            set
            {
                OnPropertyChanged(nameof(GoToString));
                OnPropertyChanged(nameof(_interval.GoTo));

                _interval.GoTo = value;
            }
        }
        public string GoToString
        {
            get
            {
                return $"Go to: {GoTo}";
            }
        }
        public int Repeat
        {
            get { return _interval.Repeat; }
            set
            {
                OnPropertyChanged(nameof(RepeatString));
                OnPropertyChanged(nameof(_interval.Repeat));

                _interval.Repeat = value;
            }
        }
        public string RepeatString
        {
            get
            {
                return $"Repeat: {Repeat}";
            }
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

            while (Distance > 0)
            {
                gpsManager.TrackDistance(cts);

                if (gpsManager.TotalDistance > 0.001)
                {
                    gpsManager.TotalDistance -= 0.001;
                    Distance -= 1;
                }
            }
        }
        public void CountdownGoTo()
        {
            if (Repeat == 0)
                return;

            Repeat--;


        }
        public void Reset()
        {
            Duration = _interval.Duration;
            Distance = _interval.Distance;
        }
    }
}
