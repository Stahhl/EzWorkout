using EzWorkout.Models;
using EzWorkout.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EzWorkout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        public WorkoutPage(WorkoutViewModel workoutViewModel)
        {
            InitializeComponent();

            BindingContext = viewModel = workoutViewModel;

            listView.SelectionChanged += SelectionChanged;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //listView.ItemsSource = viewModel.Intervals;
        }

        private WorkoutViewModel viewModel;
        private bool isLooping;

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IntervalPage(null, viewModel));

            listView.SelectedItem = null;
        }
        private async void Start_Clicked(object sender, EventArgs e)
        {
            if (isLooping == true)
                return;

            isLooping = true;

            await LoopItems();

            isLooping = false;
        }
        private async void SelectionChanged(object sender, ItemSelectionChangedEventArgs args)
        {
            await Navigation.PushAsync(new IntervalPage(
                (IntervalViewModel)listView.SelectedItem, 
                viewModel));

            listView.SelectedItem = null;
        }
        private async Task LoopItems()
        {
            IntervalViewModel current = null;
            IntervalViewModel last = null;

            for (int i = 0; i < viewModel.Intervals.Count; i++)
            {
                current = viewModel.Intervals[i];

                current.ToggleSelection();
                if (last != null)
                    last.ToggleSelection();

                last = current;

                await current.Countdown();
            }

            last.ToggleSelection();
        }
    }
}