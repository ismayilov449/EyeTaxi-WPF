using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi2019.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool IsVerified { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}\nLogin: {Login}\tPassword: {Password}\nPhone: {PhoneNumber}\nEmail: {Email}";
        }


    }
}
