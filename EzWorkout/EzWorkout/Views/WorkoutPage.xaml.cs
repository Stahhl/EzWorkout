using EzWorkout.Models;
using EzWorkout.Services;
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

            itemsVisibility();

            listView.ChildAdded += ItemsVisibility;
            listView.SelectionChanged += SelectionChanged;

            var tempArray = Enum.GetNames(typeof(IntervalType));
            TypeArray = new string[tempArray.Length - 1];
            Array.Copy(tempArray, 1, TypeArray, 0, tempArray.Length - 1);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private WorkoutViewModel viewModel;
        private CancellationTokenSource cts;
        private string[] TypeArray;

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            listView.SelectedItem = null;

            string action = await DisplayActionSheet("Type: ", "Cancel", null, TypeArray);

            if (Enum.TryParse(action, out IntervalType result) == false)
                return;

            switch (result)
            {
                case IntervalType.DISTANCE:
                    await Navigation.PushAsync(new IntervalPage(viewModel, result));
                    break;
                case IntervalType.DURATION:
                    await Navigation.PushAsync(new IntervalPage(viewModel, result));
                    break;
                case IntervalType.GOTO:
                    await Navigation.PushAsync(new IntervalPage(viewModel, result));
                    break;
                default:
                    throw new Exception("DEFAULT");
            }
        }
        private void Start_Clicked(object sender, EventArgs e)
        {
            if (cts != null)
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
        private void ItemsVisibility(object sender, EventArgs e)
        {
            itemsVisibility();
        }
        private void itemsVisibility()
        {
            var itemSource = listView;
            var arr = (listView.ItemsSource as IEnumerable<IntervalViewModel>).ToArray();

            for (int i = 0; i < viewModel.Intervals.Count; i++)
            {
                var interval = viewModel.Intervals[i];
                var obj = arr[i];

                switch (interval.Type)
                {
                    case IntervalType.DURATION:
                        break;
                    case IntervalType.DISTANCE:
                        break;
                    case IntervalType.GOTO:
                        break;
                    default:
                        throw new Exception("default");
                }
            }
        }

        private async void SelectionChanged(object sender, ItemSelectionChangedEventArgs args)
        {
            var selectedInterval = (IntervalViewModel)args.AddedItems[0];
            await Navigation.PushAsync(new IntervalPage(viewModel, selectedInterval));

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
            catch (TaskCanceledException)
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