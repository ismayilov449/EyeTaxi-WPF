using EyeTaxi2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi2019.Repository
{
    public interface IUserRepository<T>
    {
        void Add(T entity);

        IQueryable<T> FindUser();

        void Remove(T entity);

        void SaveChanges();

         

    }
}
