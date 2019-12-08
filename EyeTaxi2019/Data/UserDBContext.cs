using EyeTaxi2019.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi2019.Data
{
    public class UserDBContext : DbContext
    {
        public UserDBContext() : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UsersDb;Integrated Security=True;")
        {

        }

        public DbSet<User> Users { get; set; }


    }
}
