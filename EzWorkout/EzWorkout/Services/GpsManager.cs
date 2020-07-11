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
        private readonly GeolocationAccuracy accuracy = GeolocationAccuracy.High;

        private Location _last;
        private Location _current;
        private CancellationTokenSource _cts;

        private double _totalDistance;

        public async Task<double> GetDistance(CancellationTokenSource cts)
        {
            try
            {
                if (_cts != null)
                    cts.Cancel();

                _cts = cts;
                _last = _current != null ? _current : null;
                _current = await GetLocation();

                double dist = _last == null ? 0 : Location.CalculateDistance(_last, _current, DistanceUnits.Kilometers);

                await Task.Delay(500);

                return _totalDistance + dist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Location> GetLocation()
        {
            var request = new GeolocationRequest(accuracy);
            var result = await Geolocation.GetLocationAsync(request, _cts.Token);

            return result;
        }
    }
}
