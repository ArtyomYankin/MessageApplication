namespace MA.Service.TaskService
{
    using MA.Data.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITaskService
    {
        void AddTask(TaskMessage newTask);
        void DeleteTask(int id);
        void UpdateTask(TaskMessage updatedTask);
        Task<IEnumerable<TaskMessage>> GetAllTaskMessagesById(int userId);
        Task<IEnumerable<UserWithTasks>> GetAllTaskMessagesWithReceiver();
    }
}
