using EzWorkout.Models;
using EzWorkout.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EzWorkout.ViewModels
{
    public class IntervalViewModel : _BaseViewModel
    {
        public IntervalViewModel(Interval _interval)
        {
            interval = _interval;
            Color = Color.LightBlue;
        }

        public Interval interval;
        private Color _color;
        private bool _isSelected;

        public Color Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }
        public IntervalIntensity Intensity
        {
            get { return interval.Intensity; }
            set { SetProperty(ref interval.Intensity, value); }
        }
        public IntervalType Type
        {
            get { return interval.Type; }
            set { SetProperty(ref interval.Type, value); }
        }
        public TimeSpan Duration
        {
            get { return interval.CurrentDuration; }
            set { SetProperty(ref interval.CurrentDuration, value); }
        }
        public int Distance
        {
            get { return interval.CurrentDistance; }
            set { SetProperty(ref interval.CurrentDistance, value); }
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
        public async Task Countdown(CancellationTokenSource cts)
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
                    Duration = Duration.Subtract(TimeSpan.FromMilliseconds(max));
                    current = 0;
                }

                await Task.Delay(tick);
            }
        }
        public void Reset()
        {
            Duration = interval.Duration;
            Distance = interval.Distance;
        }
    }
}
