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
        void UpdateTask(int id, TaskMessage updatedTask);
        Task<IEnumerable<TaskMessage>> GetAllTaskMessages();
    }
}
