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
                command.CommandText = $"INSERT INTO TaskMessage (Name, Description, LastSend) VALUES ('{entity.Name}', '{entity.Description}', '{entity.LastSent}')";
                int number = command.ExecuteNonQuery();

                Console.WriteLine($"В таблицу Task добавлено объектов: {number}");
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
                Console.WriteLine($"{number}");
            }
            return null;
        }

        public async Task<IEnumerable<TaskMessage>> GetAll()
        {
            string sqlExpression = "SELECT * FROM TaskMessage";
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
                                Id =  Convert.ToInt32(reader.GetValue(0)),
                                Name = (string)reader.GetValue(1),
                                Description = (string)reader.GetValue(2),
                                LastSent = Convert.ToDateTime(reader.GetValue(3))
                            };
                            taskMessages.Add(taskMessage);
                        }
                    }
                }
            }
            return taskMessages;
        }
    
        public Task<TaskMessage> Update(int id, T entity)
        {
            using (var connection = new SqliteConnection($"Data Source={DbPath}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE TaskMessage SET Name = '{entity.Name}', Description = '{entity.Description}' WHERE Id = {id}";
                int number = command.ExecuteNonQuery();
                Console.WriteLine($"{number}");
            }
            return null;
        }
    }
}
