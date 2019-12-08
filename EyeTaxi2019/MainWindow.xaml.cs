using Microsoft.Maps.MapControl.WPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Configuration;
using System.Windows.Media.Animation;
using EyeTaxi2019.Services;
using EyeTaxi2019.Models;
using EyeTaxi2019.Repo;
using EyeTaxi2019.Views;
using EyeTaxi2019.ViewModels;

namespace BingMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        #endregion

        #region Fields

        //public LocationCollection Locations { get; set; } = new LocationCollection();

        //int TempIndexForLocationCollection = 0;

        public Pushpin UserLocationPushpin = new Pushpin() { Location = GetCurrentLocationService.CurrentLocation() };

        public MapPolyline MapPolyline = new MapPolyline()
        {
            Stroke = new SolidColorBrush(Colors.Green),
            StrokeThickness = 5,
            Opacity = 0.7,
            Tag = "Polyline",
        };

        readonly DispatcherTimer TimerForCarAnimation = new DispatcherTimer();

        public RouteResult routeResult { get; set; } = new RouteResult();

        public ObservableCollection<SearchResult> SearchResultsCollection { get; set; } = new ObservableCollection<SearchResult>();
       
        public bool DriverWaiting = true;

        public MainViewModel vm { get; set; }

        public SignUpViewModel SignUpvm { get; set; }
        public SignUpPage SignUpPage { get; set; }


        #endregion



        public MainWindow() 
        {
            InitializeComponent();
            vm = new MainViewModel(myMap, Btn_CurrentLocation, UserLocationPushpin, btnSearch, tbSearch, ToggleButtonOpen, ListViewSearch, GridMenu, DriversRepo.GetDrivers());
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {


            if (String.IsNullOrWhiteSpace(tbSearch.Text) != true)
            {
              
               
                    BeginStoryboard((Storyboard)FindResource("CloseMenu"));
                    BeginStoryboard((Storyboard)FindResource("RightMenu"));
                    
                    ToggleButtonOpen.IsChecked = true;
                
            }
            else MessageBox.Show("Enter the place name");

            ListViewSearch.ItemsSource = SearchResultsCollection;
        }

        private void Btn_Direction_Click(object sender, RoutedEventArgs e)
        {
            vm.DestinationLocation = (sender as Button).Tag as Location;

            ToggleButtonOpen.IsChecked = false;
            BeginStoryboard((Storyboard)FindResource("LeftMenu"));

            vm.DirectionCommand.Execute(new object());

          

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
            DriverWaiting = true;
           
            myMap.Focus();
            myMap.Children.Clear();
            myMap.Children.Add(UserLocationPushpin);
            AddDriversToMapService.AddDriversOnTheMap(myMap, DriversRepo.GetDrivers());
        }


        #region Zoom
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myMap.ZoomLevel = (sender as Slider).Value;
        }
        #endregion

        #region Storyboard
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleButtonOpen.IsChecked == true && GridSearchMenu.Width == 0)
            {
                BeginStoryboard((Storyboard)FindResource("OpenMenu"));
            }
            else if (ToggleButtonOpen.IsChecked == false && GridMenu.Width == 0)
            {
                BeginStoryboard((Storyboard)FindResource("LeftMenu"));

            }
            else
            {
                BeginStoryboard((Storyboard)FindResource("CloseMenu"));
            }
        }
        #endregion

        #region Region Windows State
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMaxOrNorm_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                (sender as Button).ToolTip = "Restore Down";
            }
            else
            {
                this.WindowState = WindowState.Normal;
                (sender as Button).ToolTip = "Maximize";
            }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        #endregion

        #region Map Mode
        private void AriealModeChanged_Click(object sender, MouseButtonEventArgs e)
        {
            if (ToggleBtnLabel.IsChecked == true)
            {
                myMap.Mode = new AerialMode(true);
            }
            else
            {
                myMap.Mode = new AerialMode();
            }
            ToggleBtnLabel.ToolTip = "AerialMode";
        }

        private void RoadModeChanged_Click(object sender, MouseButtonEventArgs e)
        {
            myMap.Mode = new RoadMode();
            ToggleBtnLabel.ToolTip = "RoadMode";
        }

        private void ToggleBtnLabel_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleBtnLabel.ToolTip.ToString() == "AerialMode")
                if (ToggleBtnLabel.IsChecked == true)
                    myMap.Mode = new AerialMode(true);
                else
                    myMap.Mode = new AerialMode();
        }

        #endregion

        #region Drag Move
        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        #endregion

        #region KeyDown
        private void TbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && String.IsNullOrWhiteSpace(tbSearch.Text) == true) {
                ClearFunc();
                BeginStoryboard((Storyboard)FindResource("LeftMenu"));
                ToggleButtonOpen.IsChecked = false;
            }
           

            if(e.Key == Key.Enter && String.IsNullOrWhiteSpace(tbSearch.Text) == false)
            {
                BtnSearch_Click(btnSearch, null);
            }
        }
        #endregion
    }
}