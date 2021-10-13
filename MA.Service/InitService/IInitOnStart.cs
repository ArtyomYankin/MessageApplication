namespace MA.Service.InitService
{
    using MA.Data.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IInitOnStart
    {
        Task<IEnumerable<UserWithTasks>> GetAllTaskMessagesWithReceiver();
    }
}
