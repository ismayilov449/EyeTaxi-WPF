using BingMap;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EyeTaxi2019.Views
{
    /// <summary>
    /// SignUpPage.xaml etkileşim mantığı
    /// </summary>
    public partial class SignUpPage : Page
    {
        public MainWindow Window { get; set; } = new MainWindow();
        public SignUpViewModel SignUpvm { get; set; }

        public SignUpPage()
        {
             
            InitializeComponent();
            SignUpvm = new SignUpViewModel(tbUsername, pbPassword);
            SignUpvm.MainWindow = Window;
            SignUpvm.Mainvm = Window.vm;
            SignUpvm.SignUpPage = this;

            this.DataContext = SignUpvm;

             
            // window.ShowDialog();
        }
    }
}
