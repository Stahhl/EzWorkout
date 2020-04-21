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

        }

        private ObservableCollection<WorkoutViewModel> workouts;

        public ObservableCollection<WorkoutViewModel> Workouts
        {
            get { return workouts; }
            set { this.workouts = value; }
        }
    }
}
