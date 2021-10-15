namespace MA.Service.InitService
{
    using MA.Repository;
    using Quartz;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class Initializer: IJob
    {
        private readonly IInitOnStart _initOnStart;
        public Initializer(IInitOnStart initOnStart)
        {
            _initOnStart = initOnStart;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _initOnStart.GetAllTaskMessagesWithReceiver();
        }
    }
}
