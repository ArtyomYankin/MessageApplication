using MA.Data.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MA.Repository
{
    public class TaskRepository<T> : ITaskRepository<T> where T : TaskMessage
    {
        private readonly string DbPath = Environment.GetEnvironmentVariable("Db_Path");
        public async void Add(T entity)
        {
            using (var connection = new SqliteConnection($"Data Source={DbPath}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO TaskMessage (Name, Description, LastSend, ApiType, ApiParam, UserId, FirstSend, Pereodicity) VALUES ('{entity.Name}', '{entity.Description}', '{entity.LastSent}', '{entity.ApiType}', '{entity.ApiParam}', '{entity.UserId}', '{entity.FirstSend}', '{entity.Pereodicity}')";
                    int number = command.ExecuteNonQuery();
            }
        }

        public Task<TaskMessage> Delete(int id)
        {
            using (var connection = new SqliteConnection($"Data Source={DbPath}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM TaskMessage WHERE Id = {id}";
                int number = command.ExecuteNonQuery();
            }
            return null;
        }

        public async Task<IEnumerable<UserWithTasks>> GetAllTasksWithReceiver()
        {
            string sqlExpression = $"SELECT User.Email, TaskMessage.ApiType, TaskMessage.ApiParam, TaskMessage.FirstSend, TaskMessage.Pereodicity, User.Id FROM TaskMessage INNER JOIN User ON TaskMessage.UserId=User.Id; ";
            List<UserWithTasks> usersWithTasks = new List<UserWithTasks>();

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
                            var userWithTasks = new UserWithTasks()
                            {
                                Email = reader.GetString(0),
                                ApiType = reader.GetString(1),
                                ApiParam = reader.GetString(2),
                                FirstSend = reader.GetString(3),
                                Pereodicity = reader.GetInt32(4),
                                Id = reader.GetInt32(5)
                            };
                            usersWithTasks.Add(userWithTasks);
                        }
                    }
                }
            }
            return usersWithTasks;
        }

        public async Task<IEnumerable<TaskMessage>> GetAllById(int entityId)
        {
            string sqlExpression = $"SELECT * FROM TaskMessage WHERE UserId={entityId}";
            List<TaskMessage> taskMessages = new List<TaskMessage>();

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
                            var taskMessage = new TaskMessage()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                LastSent = reader.GetString(3),
                                ApiType = reader.GetString(4),
                                ApiParam = reader.GetString(5)
                                
                            };
                            
                            taskMessages.Add(taskMessage);
                        }
                    }
                }
            }
            return taskMessages;
        }
    
        public Task<TaskMessage> Update(T entity)
        {
            using (var connection = new SqliteConnection($"Data Source={DbPath}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"" +
                    $"UPDATE TaskMessage SET Name = '{entity.Name}'," +
                    $" Description = '{entity.Description}'," +
                    $" ApiType='{entity.ApiType}', ApiParam='{entity.ApiParam}'  WHERE Id = {entity.Id}";
                int number = command.ExecuteNonQuery();
            }
            return null;
        }

        public void UpdateTime()
        {
            throw new NotImplementedException();
        }
    }
}
