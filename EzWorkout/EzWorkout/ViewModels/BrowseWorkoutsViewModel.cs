using EzWorkout.Models;
using EzWorkout.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EzWorkout.ViewModels
{
    public class BrowseWorkoutsViewModel : _BaseViewModel
    {
        public BrowseWorkoutsViewModel()
        {
            generateDummyData();
        }

        private void generateDummyData()
        {
            workouts = new ObservableCollection<WorkoutViewModel>();

            var list = new List<_Interval>();
            list.Add(new DistanceInterval(IntervalIntensity.INACTIVE, 15));
            list.Add(new DurationInterval(IntervalIntensity.MEDIUM, TimeSpan.FromSeconds(10)));
            list.Add(new DurationInterval(IntervalIntensity.HIGH, TimeSpan.FromSeconds(10)));


            workouts.Add(new WorkoutViewModel( new Workout(list){ Name = "Workout " + NumberOfWorkouts }));
            workouts.Add(new WorkoutViewModel(new Workout(list) { Name = "Workout " + NumberOfWorkouts }));
            workouts.Add(new WorkoutViewModel(new Workout(list) { Name = "Workout " + NumberOfWorkouts }));
        }

        private ObservableCollection<WorkoutViewModel> workouts;

        public ObservableCollection<WorkoutViewModel> Workouts
        {
            get { return workouts; }
            set { this.workouts = value; }
        }

        public int NumberOfWorkouts
        {
            get { return workouts.Count + 1; }
        }
    }
}
