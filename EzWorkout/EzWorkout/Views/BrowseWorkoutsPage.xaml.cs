using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EzWorkout.ViewModels;
using Syncfusion.ListView.XForms;

namespace EzWorkout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowseWorkoutsPage : ContentPage
    {
        public BrowseWorkoutsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new BrowseWorkoutsViewModel();

            listView.SelectionChanged += SelectionChanged;
        }

        private BrowseWorkoutsViewModel viewModel;

        private async void BtnNewWorkout(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new NewBookPage(viewModel)));
        }

        private async void SelectionChanged(object sender, ItemSelectionChangedEventArgs args)
        {
            await Navigation.PushAsync(new WorkoutPage((WorkoutViewModel)listView.SelectedItem));

            listView.SelectedItem = null;
        }
    }
}