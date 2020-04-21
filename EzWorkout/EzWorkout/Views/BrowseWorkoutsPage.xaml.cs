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
    public partial class BrowseWorkoutsPage : ContentPage
    {
        public BrowseWorkoutsPage()
        {
            InitializeComponent();
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new NewBookPage(viewModel)));
        }
        private async void Start_Clicked(object sender, EventArgs e)
        {
            //if (isLooping == true)
            //    return;

            //isLooping = true;

            //await LoopItems();

            //isLooping = false;
        }
    }
}