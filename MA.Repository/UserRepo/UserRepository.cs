
using MA.Data.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MA.Repository
{
    public class UserRepository<T> : IUserRepository<T> where T: User
    {
        private readonly string DbPath = Environment.GetEnvironmentVariable("Db_Path");
        public void Add(T entity)
        {
            using (var connection = new SqliteConnection($"Data Source={DbPath}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO User (Email, Password) VALUES ('{entity.Email}', '{entity.Password}')";
                int number = command.ExecuteNonQuery();

                Console.WriteLine($"В таблицу Users добавлено объектов: {number}");
            }
        }

        public User Get(T entity)
        {
            using (var connection = new SqliteConnection($"Data Source={DbPath}"))
            {
                connection.Open();

                SqliteCommand commandEmail = new SqliteCommand();

                SqliteCommand commandPassword = new SqliteCommand();

                SqliteCommand commandId = new SqliteCommand();
                commandId.Connection = connection;
                commandPassword.Connection = connection;
                commandEmail.Connection = connection;
                commandId.CommandText = $"SELECT Id FROM User WHERE Email=\"{entity.Email}\" AND Password=\"{entity.Password}\"";
                commandEmail.CommandText = $"SELECT Email FROM User WHERE Email=\"{entity.Email}\" AND Password=\"{entity.Password}\"";
                commandPassword.CommandText = $"SELECT Password FROM User WHERE Email=\"{entity.Email}\" AND Password=\"{entity.Password}\"";
                int UserId = Convert.ToInt32(commandId.ExecuteScalar());
                string UserEmail = (string)commandEmail.ExecuteScalar();
                string UserPassword = (string)commandPassword.ExecuteScalar();
                var user = new User()
                {
                    Id = UserId,
                    Email = UserEmail,
                    Password = UserPassword
                };
               
                return user;
            }
            
        }

        public async Task<User> GetById(string id)
        {
            using (var connection = new SqliteConnection($"Data Source={DbPath}"))
            {
                connection.Open();

                SqliteCommand commandId = new SqliteCommand();
                SqliteCommand commandEmail = new SqliteCommand();
                SqliteCommand commandPassword = new SqliteCommand();
                commandId.Connection = connection;
                commandPassword.Connection = connection;
                commandEmail.Connection = connection;
                commandId.CommandText = $"SELECT Id FROM User WHERE Id=\"{id}\"";
                commandEmail.CommandText = $"SELECT Email FROM User WHERE Id=\"{id}\"";
                commandPassword.CommandText = $"SELECT Password FROM User WHERE Id=\"{id}\"";
                int UserId = Convert.ToInt32(commandId.ExecuteScalar());
                string UserEmail =  (string)commandEmail.ExecuteScalar();
                string UserPassword = (string)commandPassword.ExecuteScalar();
                var user = new User()
                {
                    Id = UserId,
                    Email = UserEmail,
                    Password = UserPassword
                };

                return user;
            }
        }
    }
}
