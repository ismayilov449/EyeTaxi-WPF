using Microsoft.Maps.MapControl.WPF;
using System.Device.Location;



namespace EyeTaxi2019.Services
{
    public class GetCurrentLocationService
    {
        public static GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);

        public static Location CurrentLocation()
        {
            watcher.Start();
            return new Location(watcher.Position.Location.Latitude, watcher.Position.Location.Longitude);
        }
    }
}
