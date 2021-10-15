
using MA.Data.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MA.Repository
{
    public class UserRepository<T> : IUserRepository<T> where T : User
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
            string sqlExpression = $"SELECT * FROM User WHERE Email=\"{entity.Email}\" AND Password=\"{entity.Password}\"";
            User user = new User();
            using (var connection = new SqliteConnection($"Data Source={DbPath}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = new User()
                            {
                                Id = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                Password = reader.GetString(2)
                            };
                        }
                    }
                }

            }
            return user;
        }



        public async Task<User> GetById(string id)
        {
            string sqlExpression = $"SELECT * FROM User WHERE Id=\"{id}\"";
            User user = new User();
            using (var connection = new SqliteConnection($"Data Source={DbPath}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = new User()
                            {
                                Id = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                Password = reader.GetString(2)
                            };
                        }
                    }
                }

            }
            return user;
        }
    }
}