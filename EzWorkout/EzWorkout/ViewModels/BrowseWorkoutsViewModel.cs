using EzWorkout.Models;
using EzWorkout.Services;
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

            var list = new List<Interval>();
            list.Add(new Interval() { Intensity = IntervalIntensity.INACTIVE, Type = IntervalType.DURATION, Amount = 100 });
            list.Add(new Interval() { Intensity = IntervalIntensity.MEDIUM, Type = IntervalType.DISTANCE, Amount = 200 });
            list.Add(new Interval() { Intensity = IntervalIntensity.HIGH, Type = IntervalType.DURATION, Amount = 500 });

            workouts.Add(new WorkoutViewModel( new Workout(list){ Name = "New Workout 1" }));
            workouts.Add(new WorkoutViewModel(new Workout(list) { Name = "New Workout 2" }));
            workouts.Add(new WorkoutViewModel(new Workout(list) { Name = "New Workout 3" }));
        }

        private ObservableCollection<WorkoutViewModel> workouts;

        public ObservableCollection<WorkoutViewModel> Workouts
        {
            get { return workouts; }
            set { this.workouts = value; }
        }
    }
}
