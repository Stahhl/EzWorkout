using EzWorkout.ViewModels;
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
        }

        private WorkoutViewModel viewModel;

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
        }
        private async void Start_Clicked(object sender, EventArgs e)
        {

        }
    }
}