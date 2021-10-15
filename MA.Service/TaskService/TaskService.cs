namespace MA.Service.TaskService
{
    using MA.Data.Model;
    using MA.Repository;
    using System.Collections.Generic;
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

        public async Task<IEnumerable<UserWithTasks>> GetAllTaskMessagesWithReceiver()
        {
            return await _taskRepository.GetAllTasksWithReceiver();
        }

        public async Task<IEnumerable<TaskMessage>> GetAllTaskMessagesById(int userId)
        {
            
            return await _taskRepository.GetAllById(userId);
        }

        public async void UpdateTask(TaskMessage updatedTask)
        {
            _taskRepository.Update(updatedTask);
        }

        public void UpdateLastSent()
        {
            throw new System.NotImplementedException();
        }
    }
}
