using EyeTaxi2019.Command;
using EyeTaxi2019.Data;
using EyeTaxi2019.Models;
using EyeTaxi2019.Repository;
using EyeTaxi2019.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EyeTaxi2019.ViewModels
{
    public class RegistrationViewModel
    {
        static Random ran = new Random();
        int newran = ran.Next(100000, 999999);
        public Window MainWindow { get; set; } = new Window();
        public Page RegistrationPage { get; set; } = new Page();

        public RelayCommand SendCodeCommand { get; set; }

        public RelayCommand SignUpCommand { get; set; }

        public UserDBContext UserDBContext { get; set; }

        public UserRepository<User> UserRepo { get; set; }
        public RegistrationViewModel(TextBox firstname, TextBox lastname, TextBox phonenumber, TextBox mail, TextBox username, PasswordBox password, PasswordBox confirmpassword, TextBox verifycode,Page currentpage)
        {

            int oldran = newran;

            SendCodeCommand = new RelayCommand(e =>
            {
                 
                if (password.Password == confirmpassword.Password)
                {

                    try
                    {
                        ////////MAIL PART//////
                        SmtpClient smtp = new SmtpClient();
                        int port = int.Parse(ConfigurationManager.AppSettings["SMTP_TLS"]);
                        string host = ConfigurationManager.AppSettings["SMTP_Server_Adress"];
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Host = host;
                        smtp.Port = port;
                        string mypass = ConfigurationManager.AppSettings["MyPassword"];
                        string mylog = ConfigurationManager.AppSettings["MyLogin"];
                        smtp.Credentials = new NetworkCredential(mylog, mypass);



                        ////////MAIL MESSAGE PART/////////

                        MailMessage message = new MailMessage();
                        message.From = new MailAddress(mylog);
                        message.To.Add(mail.Text);
                        message.Subject = ConfigurationManager.AppSettings["Subject"];
                        message.Body = $@"In order to help maintain the security of your EyeTaxi account we sent you this verification code: {newran} ";
                        smtp.Send(message);
                        MessageBox.Show("Please Verify Your Email");


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            },
            checkCells=> 
            {
                if (firstname.Text.Length>1 && lastname.Text.Length>1 && phonenumber.Text.Length > 6)
                {
                    PasswordScore passwordStrengthScore = PasswordAdvisor.CheckStrength(password.Password);

                    switch (passwordStrengthScore)
                    {
                        case PasswordScore.Blank:
                        case PasswordScore.VeryWeak:
                            return false;
                        case PasswordScore.Weak:
                        case PasswordScore.Medium:
                        case PasswordScore.Strong:
                        case PasswordScore.VeryStrong:
                            if (password.Password == confirmpassword.Password) {
                                return true;
                            }
                            break;
                    }

                }

                return false;
            });


            SignUpCommand = new RelayCommand(e =>
            {


                if (verifycode.Text == oldran.ToString())
                {

                    UserDBContext = new UserDBContext();
                    UserRepo = new UserRepository<User>(UserDBContext);

                    User user = new User();
                    user.LastName = lastname.Text.ToString();
                    user.FirstName = firstname.Text.ToString();
                    user.Email = mail.Text.ToString();
                    user.PhoneNumber = phonenumber.Text.ToString();
                    user.Login = username.Text.ToString();
                    user.Password = password.Password.ToString();
                    user.IsVerified = true;

                    UserRepo.Add(user);
                    UserDBContext.SaveChanges();
                    MessageBox.Show("SUCCESS!!!");

                    currentpage.NavigationService.Navigate(new SignUpPage());
                    

                }

                else
                {
                    MessageBox.Show("Your Verification Code is not correct");
                }

            });


        }
    }
}
