namespace MA.Service.InitService
{
    using MA.Data.Model;
    using MA.Repository;
    using MessageApplication.EmailSender;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class InitOnStart : IInitOnStart
    {
        private readonly ITaskRepository<TaskMessage> _taskRepository;
        public InitOnStart(ITaskRepository<TaskMessage> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<UserWithTasks>> GetAllTaskMessagesWithReceiver()
        {
            var tasks = await _taskRepository.GetAllTasksWithReceiver();
            foreach (var task in tasks)
            {
                EmailSheduler.Start(task);
            }
            return null;
        }
    }
}
