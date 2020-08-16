using EzWorkout.Models;
using EzWorkout.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace EzWorkout.ViewModels
{
    public class BrowseWorkoutsViewModel : _BaseViewModel
    {
        public BrowseWorkoutsViewModel()
        {

        }

        public void Init(List<Workout> workouts)
        {
            _workouts = new ObservableCollection<WorkoutViewModel>();
            foreach (var item in workouts)
            {
                _workouts.Add(new WorkoutViewModel(item));
            }
            OnPropertyChanged(nameof(Workouts));
        }

        private ObservableCollection<WorkoutViewModel> _workouts;

        public ObservableCollection<WorkoutViewModel> Workouts
        {
            get { return _workouts; }
            set 
            {
                OnPropertyChanged(nameof(Workouts));

                _workouts = value; 
            }
        }

        public int NumberOfWorkouts
        {
            get { return Workouts.Count + 1; }
        }
    }
}
