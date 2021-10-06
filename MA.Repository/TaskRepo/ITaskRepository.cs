using MA.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MA.Repository
{
    public interface ITaskRepository<T>
    {
        Task<TaskMessage> Add(TaskMessage task);
        
    }
}
