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
    public class RoutesRepo
    {
        public static ObservableCollection<Route> GetRoutes()
        {
            ObservableCollection<Route> Routes;
            var json = File.ReadAllText("Routes.json");
            Routes = JsonConvert.DeserializeObject<ObservableCollection<Route>>(json);
            return Routes;
        }
    }
}
