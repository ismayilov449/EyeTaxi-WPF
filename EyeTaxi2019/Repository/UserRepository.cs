using EyeTaxi2019.Data;
using EyeTaxi2019.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi2019.Repository
{
    public class UserRepository<T> : IUserRepository<T> where T : Entity
    {
        public UserDBContext UserDBContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public UserRepository(UserDBContext Context)
        {
            UserDBContext = Context;
            DbSet = UserDBContext.Set<T>();
        }
        public void Add(T user)
        {
            DbSet.Add(user);
            UserDBContext.SaveChanges();
            
        }

          
        public IQueryable<T> FindUser()
        {
            return DbSet;
        }

        public void Remove(T user)
        {
            DbSet.Remove(user);
            UserDBContext.SaveChanges();
        }

        public void SaveChanges()
        {
            UserDBContext.SaveChanges();
        }
         
    }
}
