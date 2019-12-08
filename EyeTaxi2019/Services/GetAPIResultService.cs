using EyeTaxi2019.Models;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Windows;

namespace EyeTaxi2019.Services
{
    public class GetAPIResultService
    {
       static HttpClient client = new HttpClient();


        public static ObservableCollection<SearchResult> PlaceLocation(string PlaceName)
        {
            ObservableCollection<SearchResult> SearchResultsCollection = new ObservableCollection<SearchResult>();
            SearchResult searchResult;

            PlaceName = PlaceName.Replace(" ", "%20");

            StringBuilder sb = new StringBuilder();
            sb.Append("https://maps.googleapis.com/maps/api/place/textsearch/json?query=");
            sb.Append(PlaceName);
            sb.Append("&region=AZ&key=");
            sb.Append(ConfigurationManager.ConnectionStrings["GoogleApiKey"].ConnectionString);


            var url = sb.ToString();
            var result1 = client.GetAsync(url).GetAwaiter().GetResult();
            var str = result1.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            dynamic Coordinates = JsonConvert.DeserializeObject(str);

            foreach (var result in Coordinates.results)
            {
                searchResult = new SearchResult();
                searchResult.Adress = result["formatted_address"].ToString();
                searchResult.Name = result["name"].ToString();
                searchResult.Icon = result["icon"].ToString();
                searchResult.Raiting = double.Parse(result["rating"]?.ToString() ?? "0.0");
                searchResult.Location = new Location(double.Parse(result["geometry"]["location"]["lat"].ToString()), double.Parse(result["geometry"]["location"]["lng"].ToString()));

                SearchResultsCollection.Add(searchResult);
            }

            return SearchResultsCollection;

        }




        public static string CurrentLocationAddressName(Location location)
        {
            string TempReplace = "";

            StringBuilder sb = new StringBuilder();
            sb.Append("https://maps.googleapis.com/maps/api/geocode/json?latlng=");
            TempReplace = location.Latitude.ToString();
            TempReplace = TempReplace.Replace(",", ".");
            sb.Append(TempReplace);
            sb.Append(",");
            TempReplace = location.Longitude.ToString();
            TempReplace = TempReplace.Replace(",", ".");
            sb.Append(TempReplace);
            sb.Append("&key=");
            sb.Append(ConfigurationManager.ConnectionStrings["GoogleApiKey"].ConnectionString);





            try
            {
                dynamic JsonFile = JsonConvert.DeserializeObject(client.GetAsync(sb.ToString()).Result.Content.ReadAsStringAsync().Result);
                TempReplace = JsonFile["results"][0]["formatted_address"].ToString();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }


            return TempReplace;
        }



        public static void GetRoutePoints(Location start, Location end, RouteResult routeResult)
        {


            string tempLat = start.Latitude.ToString();
            tempLat = tempLat.Replace(",", ".");

            string tempLng = start.Longitude.ToString();
            tempLng = tempLng.Replace(",", ".");

            string StartLocationString = tempLat + "," + tempLng;

            tempLat = end.Latitude.ToString();
            tempLat = tempLat.Replace(",", ".");

            tempLng = end.Longitude.ToString();
            tempLng = tempLng.Replace(",", ".");

            string DestinationLocationString = tempLat + "," + tempLng;



            var link = ConfigurationManager.AppSettings["RouteApi"] + "wayPoint.0=" + StartLocationString + "&waypoint.1=" + DestinationLocationString + "&optmz=distance&routeAttributes=routePath&key=" + ConfigurationManager.ConnectionStrings["BingMapApiKey"].ConnectionString;
            dynamic JsonFile = JsonConvert.DeserializeObject(client.GetAsync(link).Result.Content.ReadAsStringAsync().Result);



            //Convert Json File
            foreach (var f1 in JsonFile.resourceSets)
            {
                foreach (var f2 in f1.resources)
                {
                   
                    routeResult.Duration = f2["travelDuration"] / 60;
                    routeResult.Distance = f2["travelDistance"];

                    var pointsArray = f2["routePath"]["line"]["coordinates"];


                    for (int i = 0; i < pointsArray.Count; i++)
                    {
                        routeResult.Locations.Add(new Location(double.Parse(pointsArray[i][0].ToString()), double.Parse(pointsArray[i][1].ToString())));
                    }


                    // For Degree
                    foreach (var f3 in f2["routeLegs"])
                    {

                        foreach (var f4 in f3["itineraryItems"])
                        {
                            var pointsArraymaneuverPoint = f4["maneuverPoint"]["coordinates"];


                            routeResult.ManeuverPointsForDegreesAnimation.Add(new Location(double.Parse(pointsArraymaneuverPoint[0].ToString()), double.Parse(pointsArraymaneuverPoint[1].ToString())));


                            foreach (var f5 in f4["details"])
                            {
                                try
                                {
                                    routeResult.RoutePathForDegreesAnimation.Add(int.Parse(f5["compassDegrees"].ToString()));
                                    break; // Derece sayi Maneuver Point sayina beraber olmagi ucun
                                }
                                catch (Exception)
                                {
                                   // MessageBox.Show(ex.Message + " " + "Line 265");
                                }
                            }
                        }
                    }
                }
            }
        }




    }
}