using EzWorkout.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace EzWorkout.ViewModels
{
    public class WorkoutViewModel : BaseViewModel
    {
        public WorkoutViewModel()
        {

        }

        private Workout workout;
        private ObservableCollection<Interval> intervals;

        public Workout Workout
        {
            get { return workout; }
            set { this.workout = value; }
        }
        public ObservableCollection<Interval> Intervals
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
