using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EzWorkout.Services
{
    public class GpsManager
    {
        public GpsManager()
        {
            _spoofDistance = double.Parse(AppSettingsManager.Settings["SpoofDistance"]);
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
            var request = new GeolocationRequest(accuracy, TimeSpan.FromSeconds(3));
            var result = await Geolocation.GetLocationAsync(request, _cts.Token);

            _last = _current != null ? _current : null;
            _current = result;

            double dist = _last == null ? 0 : Location.CalculateDistance(_last, _current, DistanceUnits.Kilometers);
            return  dist + _spoofDistance;
        }
    }
}
