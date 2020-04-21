using System;

using EzWorkout.Models;

namespace EzWorkout.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Workout Item { get; set; }
        public ItemDetailViewModel(Workout item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
