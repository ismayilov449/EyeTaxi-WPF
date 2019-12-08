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
    /// RegistrationPage.xaml etkileşim mantığı
    /// </summary>
    public partial class RegistrationPage : Page
    {

        public RegistrationViewModel RegistrationViewModel { get; set; }
        public RegistrationPage()
        {
            InitializeComponent();
            RegistrationViewModel = new RegistrationViewModel(tbFirstname,tbLastname,tbPhoneNumber,tbMail,tbUsername,pbPassword,pbConfirm,tbVerifyCode,this);
            this.DataContext = RegistrationViewModel;
        }
    }
}
