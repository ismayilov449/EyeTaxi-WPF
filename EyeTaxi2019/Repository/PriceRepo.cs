using EyeTaxi2019.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi2019.Repo
{
    public class PriceRepo
    {
        public static string GetPrice()
        {
            string json = File.ReadAllText("Price.json");

            return json;
        }
    }
}
