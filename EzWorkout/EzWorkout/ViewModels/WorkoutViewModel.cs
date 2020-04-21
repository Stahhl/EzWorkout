using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using EzWorkout.Models;
using EzWorkout.Views;

namespace EzWorkout.ViewModels
{
    public class WorkoutViewModel : BaseViewModel
    {
        public ObservableCollection<Workout> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public WorkoutViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Workout>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Workout>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Workout;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}