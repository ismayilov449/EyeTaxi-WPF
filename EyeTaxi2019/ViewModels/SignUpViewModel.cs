using BingMap;
using EyeTaxi2019.Command;
using EyeTaxi2019.Data;
using EyeTaxi2019.Models;
using EyeTaxi2019.Repository;
using EyeTaxi2019.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EyeTaxi2019.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        public MainViewModel Mainvm { get; set; }

        private User currentUser = new User();

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged();
            }
        }

        public Window MainWindow { get; set; } = new Window();

        public Page SignUpPage { get; set; } = new Page();
        public RelayCommand RegistrationCommand { get; set; }
        public RelayCommand LogInCommand { get; set; }


        public UserRepository<User> UserRepo { get; set; }
        public UserDBContext UserDBContext { get; set; }

        public SignUpViewModel(TextBox tbUsername, PasswordBox pbPassword)
        {


            RegistrationCommand = new RelayCommand(e =>
            {

                RegistrationPage registrationPage = new RegistrationPage();
                SignUpPage.NavigationService.Navigate(registrationPage);

            });


            LogInCommand = new RelayCommand(e =>
            {
                UserDBContext = new UserDBContext();
                UserRepo = new UserRepository<User>(UserDBContext);



                try
                {

                    if (UserRepo.FindUser().FirstOrDefault(c => (c.Login == tbUsername.Text && c.Password == pbPassword.Password)).IsVerified == true)
                    {

                        Window win = (Window)SignUpPage.Parent;
                        win.Close();


                        Mainvm.CurrentUser = UserRepo.FindUser().FirstOrDefault(c => (c.Login == tbUsername.Text && c.Password == pbPassword.Password));
                        MainWindow.DataContext = Mainvm;
                        MainWindow.ShowDialog();

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Cant find", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            },
            ckPass =>
            {

                string password = pbPassword.Password;
                PasswordScore passwordStrengthScore = PasswordAdvisor.CheckStrength(password);

                switch (passwordStrengthScore)
                {
                    case PasswordScore.Blank:
                    case PasswordScore.VeryWeak:
                        return false;
                    case PasswordScore.Weak:
                    case PasswordScore.Medium:
                    case PasswordScore.Strong:
                    case PasswordScore.VeryStrong:
                        return true;
                }


                return false;

            });
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
