namespace MA.Service.TaskService
{
    using MA.Data.Model;
    using MA.Repository;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository<TaskMessage> _taskRepository;
        public TaskService(ITaskRepository<TaskMessage> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async void AddTask(TaskMessage newTask)
        {
             _taskRepository.Add(newTask);
        }

        public async void DeleteTask(int id)
        {
            _taskRepository.Delete(id);
        }

        public async Task<IEnumerable<TaskMessage>> GetAllTaskMessages(int userId)
        {
            return await _taskRepository.GetAll(userId);
        }

        public void UpdateTask(TaskMessage updatedTask)
        {
            _taskRepository.Update(updatedTask);
        }
    }
}
