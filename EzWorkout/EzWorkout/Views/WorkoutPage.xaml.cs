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

            BindingContext = _viewModel = workoutViewModel;

            listView.SelectionChanged += SelectionChanged;
            listView.ItemDragging += ItemDragging;

            var tempArray = Enum.GetNames(typeof(IntervalType));
            _typeArray = new string[tempArray.Length - 1];
            Array.Copy(tempArray, 1, _typeArray, 0, tempArray.Length - 1);
        }

        private WorkoutViewModel _viewModel;
        private CancellationTokenSource _cts;
        private string[] _typeArray;

        private void ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if (e.Action == DragAction.Drop && e.NewIndex != e.OldIndex)
            {
                ReorderDisplay(e.OldIndex, e.NewIndex);
            }
        }
        /// <summary>
        /// Reorders the line numbers of intervals in the listview on dragging them around.
        /// </summary>
        /// <param name="oldIndex"></param>
        /// <param name="newIndex"></param>
        private void ReorderDisplay(int oldIndex, int newIndex)
        {
            _viewModel.Intervals[oldIndex].IntervalIndex = newIndex;

            //0 -> n
            if (oldIndex < newIndex)
            {
                int begin = oldIndex + 1;
                int end = newIndex + 1;

                for (int i = begin; i < end; i++)
                {
                    _viewModel.Intervals[i].IntervalIndex--;
                }
            }
            //n -> 0
            else
            {
                int begin = newIndex;
                int end = oldIndex;

                for (int i = begin; i < end; i++)
                {
                    _viewModel.Intervals[i].IntervalIndex++;
                }
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void BtnAddItem(object sender, EventArgs e)
        {
            listView.SelectedItem = null;

            string action = await DisplayActionSheet("Type: ", "Cancel", null, _typeArray);

            if (Enum.TryParse(action, out IntervalType intervalType) == false)
                return;

            await Navigation.PushAsync(new IntervalPage(_viewModel, intervalType));
        }
        private void BtnStart(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
                Reset();

                _viewModel.BtnStartText = "START";
            }
            else
            {
                _cts = new CancellationTokenSource();

                Task.Run(() => LoopItems());

                _viewModel.BtnStartText = "RESET";
            }
        }
        private async void SelectionChanged(object sender, ItemSelectionChangedEventArgs args)
        {
            var selectedInterval = (IntervalViewModel)args.AddedItems[0];
            await Navigation.PushAsync(new IntervalPage(_viewModel, selectedInterval));

            listView.SelectedItem = null;
        }
        private void LoopItems()
        {
            IntervalViewModel current = null;
            IntervalViewModel last = null;

            try
            {
                for (int i = 0; i < _viewModel.Intervals.Count; i++)
                {

                    current = _viewModel.Intervals[i];

                    current.ToggleSelection();

                    if (last != null)
                        last.ToggleSelection();

                    switch (current.Type)
                    {
                        case IntervalType.DURATION:
                            current.CountdownTime(_cts);
                            break;
                        case IntervalType.DISTANCE:
                            current.CountdownDistance(_cts);
                            break;
                        case IntervalType.GOTO:
                            if (current.Repeat > 0)
                            {
                                Reset(i);

                                current.Repeat--;
                                i = current.GoTo;
                            }
                            break;
                        default:
                            throw new NotImplementedException("default");
                    }

                    last = current;
                }

                last.ToggleSelection();
            }
            catch (TaskCanceledException)
            {
                //if (last != null)
                //    last.ToggleSelection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Reset(int loopTo = -1)
        {
            loopTo = loopTo == -1 ? _viewModel.Intervals.Count : loopTo;

            for (int i = 0; i < loopTo; i++)
            {
                _viewModel.Intervals[i].Reset();
            }
        }
    }
}