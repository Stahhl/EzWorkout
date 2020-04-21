using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EzWorkout.ViewModels
{
    public class IntervalViewModel : BaseViewModel
    {
        public IntervalViewModel()
        {

        }

        private Color myColor;

        public Color MyColor
        {
            get { return myColor; }
            set { SetProperty(ref myColor, value); }
        }
    }
}
