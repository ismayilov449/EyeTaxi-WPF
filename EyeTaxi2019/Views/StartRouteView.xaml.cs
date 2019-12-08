using EyeTaxi2019.Command;
using EyeTaxi2019.Models;
using EyeTaxi2019.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EyeTaxi2019.Views
{
    /// <summary>
    /// Interaction logic for AcceptOrNot.xaml
    /// </summary>
    public partial class StartRouteView : Window
    {

        public StartViewModel Startvm { get; set; }
        public Route CurrentRoute { get; set; }

      


        public StartRouteView(Route CurrentRoute)
        {
            Startvm = new StartViewModel(CurrentRoute,this);
            InitializeComponent();
            this.DataContext = Startvm;
            this.CurrentRoute = Startvm.CurrentRoute ;


        }
         
    }
}
