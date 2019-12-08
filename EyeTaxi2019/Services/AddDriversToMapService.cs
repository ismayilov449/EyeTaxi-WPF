using EyeTaxi2019.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EyeTaxi2019.Services
{
    public class AddDriversToMapService
    {
        public static void AddDriversOnTheMap(Map map, ObservableCollection<Driver> Drivers)
        {
            foreach (var Driver in Drivers)
            {
                Pushpin driver = new Pushpin
                {
                    ToolTip = Driver.Name,
                    Location = Driver.Location,
                    Template = Application.Current.FindResource("TaxiPushpin") as ControlTemplate,
                    Tag = Driver.Id
                };
                MapLayer.SetPositionOffset(driver, new Point(0, 20));
                map.Children.Add(driver);
            };
        }
    }
}
