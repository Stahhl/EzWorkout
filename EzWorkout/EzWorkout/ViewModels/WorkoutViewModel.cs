using EzWorkout.Models;
using EzWorkout.Services;
using Syncfusion.ListView.XForms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using SelectionMode = Syncfusion.ListView.XForms.SelectionMode;

namespace EzWorkout.ViewModels
{
    public class WorkoutViewModel : _BaseViewModel
    {
        public WorkoutViewModel(Workout workout)
        {
            _intervals = new ObservableCollection<IntervalViewModel>();

            Workout = workout;

            for (int i = 0; i < workout.Intervals.Count; i++)
            {
                Intervals.Add(new IntervalViewModel(workout.Intervals[i], i));
            }

            _intervals.CollectionChanged += OnIntervalsChanged;
        }

        private Workout _workout;
        private ObservableCollection<IntervalViewModel> _intervals;
        private string _btnStartText = "START";
        private bool _isLooping;

        private void OnIntervalsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Intervals));
            OnPropertyChanged(nameof(Description));
        }
        public bool IsLooping
        {
            get { return _isLooping; }
            set
            {
                _isLooping = value;

                OnPropertyChanged(nameof(IsLooping));
            }
        }
        public string BtnStartText
        {
            get { return _btnStartText; }
            set 
            {
                _btnStartText = value;

                OnPropertyChanged(nameof(BtnStartText));
            }
        }
        public Workout Workout
        {
            get { return _workout; }
            set
            {
                this._workout = value;
            }
        }
        public ObservableCollection<IntervalViewModel> Intervals
        {
            get { return _intervals; }
        }
        public string Description
        {
            get { return $"{_intervals.Count} intervals"; }
        }
        public DragStartMode DragStartMode
        {
            get
            {
                if (IsLooping)
                    return DragStartMode.None;

                return DragStartMode.OnHold;
            }
        }
        public SelectionMode SelectionMode
        {
            get
            {
                if (IsLooping)
                    return SelectionMode.None;

                return SelectionMode.Single;
            }
        }
    }
}
