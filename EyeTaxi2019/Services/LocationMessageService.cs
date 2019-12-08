using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi2019.Services
{
    static class LocationMessageService
    {
        private static GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);

        public static Location CurrentLocation()
        {

            var whereat = watcher.Position.Location;

            watcher.Start();


            System.Threading.Thread.Sleep(1000);
            return new Location(watcher.Position.Location.Latitude, watcher.Position.Location.Longitude);


        }
    }
}
