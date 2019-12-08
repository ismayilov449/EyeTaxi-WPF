using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyeTaxi2019.Models
{
    [Serializable]
    public class Driver : Entity
    {

        public Driver(int id, string name, string surname, string phoneNumber, string carModel, string carVendor, string carNumber, double rating, Location location)
        {
            Id = id;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            CarModel = carModel;
            CarVendor = carVendor;
            CarNumber = carNumber;
            Rating = rating;
            Location = location;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string CarModel { get; set; }
        public string CarVendor { get; set; }
        public string CarNumber { get; set; }
        public double Rating { get; set; }
        public Location Location { get; set; }


    }
}
