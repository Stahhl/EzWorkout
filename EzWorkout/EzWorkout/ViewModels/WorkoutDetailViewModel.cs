using System;

using EzWorkout.Models;

namespace EzWorkout.ViewModels
{
    public class WorkoutDetailViewModel : BaseViewModel
    {
        public Workout Item { get; set; }
        public WorkoutDetailViewModel(Workout item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
