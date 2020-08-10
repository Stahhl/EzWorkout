using EzWorkout.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace EzWorkout.ViewModels
{
    public class WorkoutViewModel : _BaseViewModel
    {
        public WorkoutViewModel(Workout workout)
        {
            _intervals = new ObservableCollection<IntervalViewModel>();

            Workout = workout;

            for (int i = 0; i < workout.Intervals.Count; i++)
            {
                Intervals.Add(new IntervalViewModel(workout.Intervals[i], i));
            }

            _intervals.CollectionChanged += OnIntervalsChanged;
        }

        private void OnIntervalsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Intervals));
            OnPropertyChanged(nameof(Description));
        }

        private Workout _workout;
        private ObservableCollection<IntervalViewModel> _intervals;
        private string _btnStartText = "START";

        public string BtnStartText
        {
            get { return _btnStartText; }
            set { SetProperty(ref _btnStartText, value); }
        }
        public Workout Workout
        {
            get { return _workout; }
            set 
            { 
                this._workout = value;
            }
        }
        public ObservableCollection<IntervalViewModel> Intervals
        {
            get { return _intervals; }
        }
        public string Description
        {
            get { return $"{_intervals.Count} intervals"; }
        }
    }
}
