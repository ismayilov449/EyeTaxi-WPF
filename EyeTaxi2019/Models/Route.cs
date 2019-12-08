using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi2019.Models
{
    [Serializable]
    public class Route : Entity
    {
        public Route()
        {

        }

        public  Route(int id, double distance, double duration, double price, Driver driver, Location startLocation, Location endLocation,string startLocationName,string endLocationName)
        {
            Id = id;
            Distance = distance;
            Duration = duration;
            Price = price;
            Driver = driver;
            StartLocation = startLocation;
            EndLocation = endLocation;
            StartLocationName = startLocationName;
            EndLocationName = endLocationName;

        }

        public double Distance { get; set; }
        public double Duration { get; set; }
        public double Price { get; set; }
        public Driver Driver { get; set; }
        public Location StartLocation { get; set; }
        public string StartLocationName { get; set; }
        public Location EndLocation { get; set; }
        public string EndLocationName { get; set; }

    }
}
