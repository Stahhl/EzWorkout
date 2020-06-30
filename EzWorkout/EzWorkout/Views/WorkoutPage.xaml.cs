using EzWorkout.Models;
using EzWorkout.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
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
        }

        private WorkoutViewModel viewModel;
        private CancellationTokenSource cts;

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IntervalPage(null, viewModel));

            listView.SelectedItem = null;
        }
        private void Start_Clicked(object sender, EventArgs e)
        {
            if(cts != null)
            {
                cts.Cancel();
                cts = null;

                viewModel.BtnStartText = "START";
            }
            else
            {
                cts = new CancellationTokenSource();

                Task.Run(() => LoopItems());

                viewModel.BtnStartText = "RESET";
            }
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

            try
            {
                for (int i = 0; i < viewModel.Intervals.Count; i++)
                {

                    current = viewModel.Intervals[i];

                    current.ToggleSelection();

                    if (last != null)
                        last.ToggleSelection();

                    last = current;

                    await current.Countdown(cts);
                }

                last.ToggleSelection();

                Reset();
            }
            catch(TaskCanceledException)
            {
                if (last != null)
                    last.ToggleSelection();

                Reset();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Reset()
        {
            foreach (var interval in viewModel.Intervals)
            {
                interval.Reset();
            }
        }
    }
}