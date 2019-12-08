using EyeTaxi2019.Models;
using EyeTaxi2019.Repo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi2019.Services
{
    public class WriteToFileService
    {
        public static void UpdateRoutes(Route CurrentRoute)
        {
            var Routes = RoutesRepo.GetRoutes();
            if (Routes != null)
            {
                if (Routes.Count > 0)
                {
                    CurrentRoute.Id = Routes[Routes.Count - 1].Id + 1;
                }
            }
            else Routes = new ObservableCollection<Route>();
                Routes.Add(CurrentRoute);

                var json = JsonConvert.SerializeObject(Routes);


                File.WriteAllText("Routes.json", json);
             
        }



        public static void UpdateDrivers(ObservableCollection<Driver> Drivers, int RatingValue, Driver CurrentDriver)
        {

            if (RatingValue != 0)
            {

                foreach (var driver in Drivers)
                {
                    if (driver.Id == CurrentDriver.Id)
                    {
                        driver.Rating = (driver.Rating + RatingValue) / 2;
                        break;
                    }
                }
            }

            var json = JsonConvert.SerializeObject(Drivers);

            File.WriteAllText("Drivers.json", json);

        }

    }
}
