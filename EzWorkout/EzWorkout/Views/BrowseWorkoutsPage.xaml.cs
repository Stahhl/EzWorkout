﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EzWorkout.ViewModels;
using Syncfusion.ListView.XForms;
using EzWorkout.Models;

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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //await App.Database.InitializeAsync();
            var list = await App.Database.GetItemsAsync();
            viewModel.Init(list);
            //listView.ItemsSource = await App.Database.GetItemsAsync();
        }

        private async void BtnNewWorkout(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Input:", "Workout name?", initialValue: "Workout " + viewModel.NumberOfWorkouts);

            await Navigation.PushAsync(new WorkoutPage(new WorkoutViewModel(new Workout() { Name = result })));
        }

        private async void SelectionChanged(object sender, ItemSelectionChangedEventArgs args)
        {
            await Navigation.PushAsync(new WorkoutPage((WorkoutViewModel)listView.SelectedItem));

            listView.SelectedItem = null;
        }
    }
}