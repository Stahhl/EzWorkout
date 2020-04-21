using EzWorkout.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EzWorkout.ViewModels
{
    public class WorkoutViewModel : BaseViewModel
    {
        public WorkoutViewModel()
        {

        }

        private ObservableCollection<Interval> intervals;

        public ObservableCollection<Interval> Intervals
        {
            get { return intervals; }
            set { this.intervals = value; }
        }
    }
}
