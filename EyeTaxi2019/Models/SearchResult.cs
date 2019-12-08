using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EyeTaxi2019.Models
{
    public class SearchResult : Entity
    {
        public Location Location { get; set; }
        public string Adress { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public double Raiting { get; set; }

        public SearchResult()
        {

        }
    }
}
