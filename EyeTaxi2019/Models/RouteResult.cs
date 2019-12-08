using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi2019.Models
{
    public class RouteResult : Entity
    {
        public LocationCollection Locations { get; set; } = new LocationCollection();
        public LocationCollection ManeuverPointsForDegreesAnimation { get; set; } = new LocationCollection();
        public List<int> RoutePathForDegreesAnimation { get; set; } = new List<int>();
        public double Duration { get; set; } = 0;
        public double Distance { get; set; } = 0;

        public RouteResult()
        {

        }
    }
}
