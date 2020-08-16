using EzWorkout.Models;
using EzWorkout.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Testing
{
    public class ViewModelTests
    {
        [Fact]
        public void SetPropTest_01()
        {
            var viewModel = new IntervalViewModel(new DistanceInterval(), 0);

            viewModel.Distance = 1;
            viewModel.Distance = 2;
            viewModel.Distance = 3;

        }
        [Fact]
        public void SetPropTest_02()
        {
            var viewModel = new StopwatchViewModel();

            viewModel.Distance = "1";
            viewModel.Distance = "2";
            viewModel.Distance = "3";


        }
        [Fact]
        public void DummyDataTest_01()
        {
            var viewModel = new BrowseWorkoutsViewModel();
        }
    }
}
