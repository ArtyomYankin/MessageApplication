using MA.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MA.Service
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        User GetUser(User user);
        Task<User> GetById(string id);
    }
}
