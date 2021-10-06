using MA.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MA.Repository
{
    public interface IUserRepository<T> where T: User
    {
        public void Add(T entity);
        User Get(T entity);
        Task<User> GetById(string id);
    }
}
