using MA.Data.Model;
using MA.Repository;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MA.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _loginRepository;
        public UserService(IUserRepository<User> loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public async Task<User> AddUser(User user)
        {
            _loginRepository.Add(user);
            return null;
        }

        public async Task<User> GetById(string id)
        {
            var user = await _loginRepository.GetById(id);
            return user;
        }

        public User GetUser(User user)
        {
            var userToGet = _loginRepository.Get(user);
            return userToGet;
        }

        public async Task<string> HashPassword(string password)
        {
            HashAlgorithm algorithm = new SHA256CryptoServiceProvider();


            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);

        }
    }
}
