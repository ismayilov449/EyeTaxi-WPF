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
    /// Interaction logic for EndRouteView.xaml
    /// </summary>
    public partial class EndRouteView : Window
    {
        public int RatingValue { get; set; }

        public EndViewModel Endvm;

        public EndRouteView()
        {
            Endvm = new EndViewModel(this);

            InitializeComponent();

            this.DataContext = Endvm;
        }

       
        private void RatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Endvm.RatingValue = RatingBar.Value;
        }
    }
}
