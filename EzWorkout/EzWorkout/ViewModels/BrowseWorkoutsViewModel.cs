using EzWorkout.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EzWorkout.ViewModels
{
    public class BrowseWorkoutsViewModel : BaseViewModel
    {
        public BrowseWorkoutsViewModel()
        {
            generateDummyData();
        }

        private void generateDummyData()
        {
            workouts = new ObservableCollection<WorkoutViewModel>();

            workouts.Add(new WorkoutViewModel() { Workout = new Workout() { Name = "New Workout 1" }, Intervals = new ObservableCollection<Interval>() });
            workouts.Add(new WorkoutViewModel() { Workout = new Workout() { Name = "New Workout 2" }, Intervals = new ObservableCollection<Interval>() });
            workouts.Add(new WorkoutViewModel() { Workout = new Workout() { Name = "New Workout 3" }, Intervals = new ObservableCollection<Interval>() });
        }

        private ObservableCollection<WorkoutViewModel> workouts;

        public ObservableCollection<WorkoutViewModel> Workouts
        {
            get { return workouts; }
            set { this.workouts = value; }
        }
    }
}
