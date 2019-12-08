using EyeTaxi2019.Models;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EyeTaxi2019.Repo
{
    public class DriversRepo
    {
        public static ObservableCollection<Driver> GetDrivers()
        {
            ObservableCollection<Driver> Drivers;
            var json = File.ReadAllText("Drivers.json");
            Drivers = JsonConvert.DeserializeObject<ObservableCollection<Driver>>(json);

            return Drivers;
        }
    }
}
