using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;

namespace EzWorkout.Services
{
    public class GpsManager
    {
        public GpsManager()
        {
            AskPermission();
            _spoofDistance = double.Parse(AppSettingsManager.Settings["SpoofDistance"], CultureInfo.InvariantCulture);
        }

        private void AskPermission()
        {
            //MainThread.BeginInvokeOnMainThread(async () =>
            //{
            //    var status = await cross
            //});
        }

        private const GeolocationAccuracy accuracy = GeolocationAccuracy.High;

        private Location _last;
        private Location _current;
        private CancellationTokenSource _cts;

        private double _spoofDistance;

        public double TotalDistance { get; set; }

        public async void TrackDistance(CancellationTokenSource cts)
        {
            try
            {
                if (_cts != null)
                    return;
                else
                    _cts = cts;

                while (cts.Token.IsCancellationRequested == false)
                {
                    TotalDistance += await UpdateDistance(cts);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<double> UpdateDistance(CancellationTokenSource cts)
        {
            if(await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>() != PermissionStatus.Granted)
            {
                await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
            }

            var request = new GeolocationRequest(accuracy, TimeSpan.FromSeconds(3));
            var result = await Geolocation.GetLocationAsync(request, _cts.Token);

            _last = _current != null ? _current : null;
            _current = result;

            var dist = _last == null ? 0 : Location.CalculateDistance(_last, _current, DistanceUnits.Kilometers);
            var tot = dist + _spoofDistance;

            return  tot;
        }
    }
}
