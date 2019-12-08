using EyeTaxi2019.Command;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyeTaxi2019.ViewModels
{
    public class EndViewModel
    {
        public int RatingValue { get; set; }
        public RelayCommand CloseCommand { get; set; }

        public RelayCommand ValueChangedCommand { get; set; }
        public EndViewModel(Window window)
        {
                
            CloseCommand = new RelayCommand(e => {

               
                window.Close();
                
            });

           

        }

    }
}
