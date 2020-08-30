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
using System.Diagnostics;

namespace EzWorkout.Services
{
    public class GpsManager
    {
        public GpsManager()
        {
            _timer = new Stopwatch();
            _spoofDistance = double.Parse(AppSettingsManager.Settings["SpoofDistance"], CultureInfo.InvariantCulture);
        }

        private const GeolocationAccuracy accuracy = GeolocationAccuracy.High;
        private const int maxTimer = 3000;

        private Location _last;
        private Location _current;
        private double _spoofDistance;
        Stopwatch _timer;

        private static CancellationTokenSource _cts;
        public double TotalDistance { get; set; }

        public async void TrackDistance(CancellationTokenSource cts)
        {
            if (_cts != null)
                return;
            else
                _cts = cts;

            while (_cts.Token.IsCancellationRequested == false)
            {
                _timer.Start();
                TotalDistance += await UpdateDistance(_cts);
                _timer.Stop();

                int sleep = maxTimer - (int)_timer.ElapsedMilliseconds;

                if (sleep > 0)
                    Thread.Sleep(sleep);
            }
        }

        public async Task<double> UpdateDistance(CancellationTokenSource cts)
        {
            if (await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>() != PermissionStatus.Granted)
            {
                await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
            }

            var request = new GeolocationRequest(accuracy, TimeSpan.FromMilliseconds(maxTimer));
            var result = await Geolocation.GetLocationAsync(request, cts.Token);

            _last = _current != null ? _current : null;
            _current = result;

            var dist = _last == null ? 0 : Location.CalculateDistance(_last, _current, DistanceUnits.Kilometers);
            var tot = dist + _spoofDistance;

            return tot;
        }
    }
}
