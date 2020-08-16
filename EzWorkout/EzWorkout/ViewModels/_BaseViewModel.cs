using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using EzWorkout.Models;
using EzWorkout.Services;

namespace EzWorkout.ViewModels
{
    public abstract class _BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set 
            {
                OnPropertyChanged(nameof(IsBusy));

                isBusy = value;
            }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set 
            {
                OnPropertyChanged(nameof(Title));

                title = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
