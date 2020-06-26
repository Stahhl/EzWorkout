using EzWorkout.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace EzWorkout.ViewModels
{
    public class WorkoutViewModel : _BaseViewModel
    {
        public WorkoutViewModel(Workout workout)
        {
            Intervals = new ObservableCollection<IntervalViewModel>();

            Workout = workout;

            foreach (var interval in workout.Intervals)
            {
                Intervals.Add(new IntervalViewModel(interval));
            }
        }

        private Workout workout;
        private ObservableCollection<IntervalViewModel> intervals;

        private string _startBtnText = "START";
        public string StartBtnText
        {
            get { return _startBtnText; }
            set { SetProperty(ref _startBtnText, value); }
        }
        public Workout Workout
        {
            get { return workout; }
            set { this.workout = value; }
        }
        public ObservableCollection<IntervalViewModel> Intervals
        {
            get { return intervals; }
            set { this.intervals = value; }
        }
        public int NumberOfIntervals
        {
            get { return intervals.Count; }
        }
    }
}
