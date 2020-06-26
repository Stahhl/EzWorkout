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

            var list = new List<Interval>();
            list.Add(new Interval(IntervalIntensity.INACTIVE, TimeSpan.FromSeconds(10)));
            list.Add(new Interval(IntervalIntensity.MEDIUM, TimeSpan.FromSeconds(10)));
            list.Add(new Interval(IntervalIntensity.HIGH, TimeSpan.FromSeconds(10)));


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
