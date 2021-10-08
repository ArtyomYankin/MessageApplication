using MA.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MA.Repository
{
    public interface ITaskRepository<T> where T: TaskMessage
    {
        void Add(T entity);
        Task<TaskMessage> Delete(int id);
        Task<TaskMessage> Update(int id, T entity);
        Task<IEnumerable<TaskMessage>> GetAll();
        
    }
}
