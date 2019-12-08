using EyeTaxi2019.Command;
using EyeTaxi2019.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyeTaxi2019.ViewModels
{
    public class StartViewModel : INotifyPropertyChanged
    {

        private Route currentRoute;

        public Route CurrentRoute
        {
            get { return currentRoute; }
            set
            {
                currentRoute = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CloseCommand { get; set; }

        public RelayCommand AcceptCommand { get; set; }


        public StartViewModel(Route CurrentRoute,Window window)
        {

            currentRoute = CurrentRoute;


            CloseCommand = new RelayCommand(e => {
                
                window.Close();

            });

            AcceptCommand = new RelayCommand(e => {
                
                window.DialogResult = true;

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
