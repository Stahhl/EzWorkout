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

            workouts.Add(new WorkoutViewModel(new Workout() { Name = "New Workout 1" }));
            workouts.Add(new WorkoutViewModel(new Workout() { Name = "New Workout 2" }));
            workouts.Add(new WorkoutViewModel(new Workout() { Name = "New Workout 3" }));
        }

        private ObservableCollection<WorkoutViewModel> workouts;

        public ObservableCollection<WorkoutViewModel> Workouts
        {
            get { return workouts; }
            set { this.workouts = value; }
        }
    }
}
