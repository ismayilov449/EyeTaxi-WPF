using EyeTaxi2019.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi2019.Services
{
    public  class GetTaxiService
    {

        public static double DistanceOfBetweenCoordinates(Location DestinationLocation)
        {
            var sCoord = new GeoCoordinate(GetCurrentLocationService.CurrentLocation().Latitude, GetCurrentLocationService.CurrentLocation().Longitude);
            var eCoord = new GeoCoordinate(DestinationLocation.Latitude, DestinationLocation.Longitude);

            return sCoord.GetDistanceTo(eCoord);
        }


        public static Driver GiveMeDriver(ObservableCollection<Driver> Drivers)
        {
            Driver CurrentDriver = Drivers[0];
            double Distance =  DistanceOfBetweenCoordinates(Drivers[0].Location) / 1000;


            foreach (var Driver in Drivers)
            {
                if (GetTaxiService.DistanceOfBetweenCoordinates(Driver.Location) / 1000 < Distance)
                {
                    Distance = GetTaxiService.DistanceOfBetweenCoordinates(Driver.Location) / 1000;
                    CurrentDriver = Driver;
                }
            }

            return CurrentDriver;
        }


    }
}
