using EyeTaxi2019.Command;
using EyeTaxi2019.Models;
using EyeTaxi2019.Repo;
using EyeTaxi2019.Services;
using EyeTaxi2019.Views;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace EyeTaxi2019.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {


        public ApplicationIdCredentialsProvider Provider = new ApplicationIdCredentialsProvider(ConfigurationManager.ConnectionStrings["BingMapApiKey"].ConnectionString);

        public Location DestinationLocation { get; set; }

        public Map MyMap { get; set; }

        public int TempIndexForLocationCollection = 0;

        public Pushpin UserLocationPushpin;

        private User currentUser ;

        public User CurrentUser
        {
            get { return currentUser ; }
            set
            {
                currentUser = value;
                OnPropertyChanged();
            }
        }


        public bool DriverWaiting = true;

        readonly DispatcherTimer TimerForCarAnimation = new DispatcherTimer();

        public Route CurrentRoute { get; set; }

        public int CarRotateForDegreeAnimation { get; set; }

        public int IdMax { get; set; }

        public RouteResult routeResult { get; set; } = new RouteResult();

        MapPolyline MapPolyline = new MapPolyline()
        {
            Stroke = new SolidColorBrush(Colors.Green),
            StrokeThickness = 5,
            Opacity = 0.7,
            Tag = "Polyline",
        };

        public ObservableCollection<Driver> DriversVM { get; set; } = new ObservableCollection<Driver>();

        private ObservableCollection<Route> history = RoutesRepo.GetRoutes();

        public ObservableCollection<Route> History
        {
            get { return history; }
            set
            {
                history = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<SearchResult> SearchResultsCollection { get; set; } = new ObservableCollection<SearchResult>();



        public RelayCommand CurrentLocationCommand { get; set; }

        public RelayCommand SearchCommand { get; set; }

        public RelayCommand DirectionCommand { get; set; }

         

        private string currentLocationAddress;

        public string CurrentLocationAddress
        {
            get { return currentLocationAddress; }
            set { currentLocationAddress = value; }
        }


        public MainViewModel(Map myMap, Button CurrentLocationBtn, Pushpin UserLocationPushpin, Button btnSearch, TextBox tbSearch, ToggleButton ToggleButtonOpen, ListView ListViewSearch, Grid GridMenu,ObservableCollection<Driver> Drivers)
        {
            MyMap = myMap;
            GetCurrentLocationService.CurrentLocation();

            MyMap.CredentialsProvider = Provider;

            AddDriversToMapService.AddDriversOnTheMap(myMap, Drivers);
            DriversVM = Drivers;

            while (true)
            {
                if (GetCurrentLocationService.CurrentLocation().ToString() != "NaN,NaN,0")
                    break;
            }

            

            CurrentLocationCommand = new RelayCommand(e =>
            {
                CurrentLocationAddress = GetAPIResultService.CurrentLocationAddressName(GetCurrentLocationService.CurrentLocation());
                CurrentLocationBtn.ToolTip = CurrentLocationAddress;


                //User
                if (UserLocationPushpin == null)
                {
                    UserLocationPushpin = new Pushpin()
                    {
                        ToolTip = "You are here!",
                        Template = Application.Current.FindResource("UserPushpin") as ControlTemplate,
                        Tag = "User",
                    };
                    MapLayer.SetPositionOffset(UserLocationPushpin, new Point(0, 20));
                    myMap.Children.Add(UserLocationPushpin);
                }

                UserLocationPushpin.Location = GetCurrentLocationService.CurrentLocation();

                myMap = MyMap;

                myMap.Center = new Location(GetCurrentLocationService.CurrentLocation().Latitude, GetCurrentLocationService.CurrentLocation().Longitude);
                myMap.ZoomLevel = 11;
                myMap.Focus();

            });
            CurrentLocationCommand.Execute(new object());

            SearchCommand = new RelayCommand(e =>
             {
                 ClearFunc();

                 if (String.IsNullOrWhiteSpace(tbSearch.Text) != true)
                 {
                     try
                     {

                         SearchResultsCollection = GetAPIResultService.PlaceLocation(tbSearch.Text);

                         for (int i = 0; i < SearchResultsCollection.Count; i++)
                         {
                             Pushpin PlaceLocationPushpin = new Pushpin()
                             {
                                 Location = SearchResultsCollection[i].Location,
                                 ToolTip = SearchResultsCollection[i].Name,
                                 Tag = "End Location",
                             };


                             PlaceLocationPushpin.MouseDown += PlaceLocationPushpin_MouseDown;

                             myMap.Children.Add(PlaceLocationPushpin);
                         }



                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message);
                         return;
                     }


                     if (SearchResultsCollection.Count > 0)
                     {

                         //FrameworkElement beginStoryboard = new FrameworkElement();
                         //beginStoryboard.BeginStoryboard((Storyboard)Application.Current.FindResource("CloseMenu"));
                         //beginStoryboard.BeginStoryboard((Storyboard)Application.Current.FindResource("RigthMenu"));



                         //BeginStoryboard((Storyboard)FindResource("CloseMenu"));
                         //BeginStoryboard((Storyboard)FindResource("RightMenu"));
                         ToggleButtonOpen.IsChecked = true;
                     }
                 }
                 else MessageBox.Show("Enter the place name");

                 ListViewSearch.ItemsSource = SearchResultsCollection;


             });


            DirectionCommand = new RelayCommand(e => {
              
                ClearFunc();
                RouteResult temprouteResult = new RouteResult();
                Route tempRoute = new Route();

                GetAPIResultService.GetRoutePoints(GetCurrentLocationService.CurrentLocation(),DestinationLocation, temprouteResult);

                 
                tempRoute = new Route(IdMax, temprouteResult.Distance, temprouteResult.Duration, Double.Parse(PriceRepo.GetPrice()) * temprouteResult.Distance,
                    GetTaxiService.GiveMeDriver(Drivers), GetCurrentLocationService.CurrentLocation(),
                    DestinationLocation, CurrentLocationAddress, GetAPIResultService.CurrentLocationAddressName(DestinationLocation));



                GetAPIResultService.GetRoutePoints(GetTaxiService.GiveMeDriver(Drivers).Location, GetCurrentLocationService.CurrentLocation(), routeResult);

                CurrentRoute = new Route(IdMax, temprouteResult.Distance, temprouteResult.Duration, Double.Parse(PriceRepo.GetPrice()) * temprouteResult.Distance,
                    GetTaxiService.GiveMeDriver(Drivers), GetCurrentLocationService.CurrentLocation(),
                    DestinationLocation, CurrentLocationAddress, GetAPIResultService.CurrentLocationAddressName(DestinationLocation));


                StartRouteView ACCORNOT = new StartRouteView(tempRoute);
                 
               
               
                MapPolyline.Locations = routeResult.Locations;
                MyMap.Children.Add(MapPolyline);

                if (ACCORNOT.ShowDialog() == true)
                {
                    TimerForCarAnimation.Interval = new TimeSpan(0, 0, 0, 0, 300);
                    TimerForCarAnimation.Tick += TimerForCarAnimation_Tick;
                    TimerForCarAnimation.Start();
                }
                else
                {
                    ClearFunc();
                }

            });


            myMap = MyMap;
        }



        private void PlaceLocationPushpin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            DestinationLocation = (sender as Pushpin).Location;

        }


        private void ClearFunc()
        {
            TimerForCarAnimation.Stop();
            routeResult.Locations.Clear();
            routeResult.ManeuverPointsForDegreesAnimation.Clear();
            routeResult.RoutePathForDegreesAnimation.Clear();
            routeResult.Distance = 0;
            routeResult.Duration = 0;
            if (SearchResultsCollection != null)
            {
                SearchResultsCollection.Clear();
            }
            TempIndexForLocationCollection = 0;
            DriverWaiting = true;

            MyMap.Focus();
            MyMap.Children.Clear();
            MyMap.Children.Add(new Pushpin() { Location = GetCurrentLocationService.CurrentLocation()});
            AddDriversToMapService.AddDriversOnTheMap(MyMap, DriversRepo.GetDrivers());
        }


        private void TimerForCarAnimation_Tick(object sender, EventArgs e)
        {
            if (routeResult.Locations.Count == 0)
                return;


            if (TempIndexForLocationCollection == 0)
            {
                MyMap.SetView(routeResult.Locations, new Thickness(200), 0);
            }



            // Dongelerde mashinin istiqametini mueyyen etmek ucun
            if (TempIndexForLocationCollection != routeResult.Locations.Count)
            {

                for (int i = 0; i < routeResult.ManeuverPointsForDegreesAnimation.Count; i++)
                {
                    if (routeResult.ManeuverPointsForDegreesAnimation[i] == routeResult.Locations[TempIndexForLocationCollection])
                    {
                        CarRotateForDegreeAnimation = routeResult.RoutePathForDegreesAnimation[i] + 93;
                        routeResult.ManeuverPointsForDegreesAnimation.RemoveAt(i); // keciymiz yollari silirem.
                        routeResult.RoutePathForDegreesAnimation.RemoveAt(i);
                        break;
                    }
                }



                foreach (var child in MyMap.Children)
                {
                    if (child is Pushpin updateTaxiLocation) // Taxi going
                    {
                        if (updateTaxiLocation.Tag?.ToString() == CurrentRoute.Driver.Id.ToString())
                        {
                            MyMap.Children.Remove(updateTaxiLocation);

                            updateTaxiLocation.Location = routeResult.Locations[TempIndexForLocationCollection];
                            updateTaxiLocation.Template = Application.Current.FindResource("TaxiPushpin") as ControlTemplate;
                            MapLayer.SetPositionOffset(updateTaxiLocation, new Point(0, 20));
                            MyMap.Children.Add(updateTaxiLocation);
                            break;
                        }
                    }


                }


                //foreach (var child in myMap.Children)
                //{
                //    if (child is MapPolyline RoutePolyline)
                //    {
                //        RoutePolyline.Locations.RemoveAt(TempIndexForLocationCollection);
                //    }
                //}

                CurrentRoute.Driver.Location = routeResult.Locations[TempIndexForLocationCollection];
                TempIndexForLocationCollection++;

            }
            else
            {


                if (DriverWaiting)
                {
                    ClearFunc();
                    GetAPIResultService.GetRoutePoints(GetCurrentLocationService.CurrentLocation(), DestinationLocation, routeResult);

                    //Add Polyline ( RoutePath )
                    MapPolyline.Locations = routeResult.Locations;
                    MyMap.Children.Add(MapPolyline);
                    MyMap.Children.Add(new Pushpin() { Location = DestinationLocation, ToolTip = GetAPIResultService.CurrentLocationAddressName(DestinationLocation).ToString() });

                    DriverWaiting = false;
                    TimerForCarAnimation.Start();
                }
                else
                {

                    EndRouteView RatingView = new EndRouteView();

                    RatingView.ShowDialog();



                     WriteToFileService.UpdateRoutes(CurrentRoute);

                     WriteToFileService.UpdateDrivers(DriversVM, RatingView.Endvm.RatingValue , CurrentRoute.Driver);


                     History = RoutesRepo.GetRoutes();

                     ClearFunc();
                }

            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

    }
}
