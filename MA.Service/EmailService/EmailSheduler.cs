namespace MessageApplication.EmailSender
{
    using MA.Data.Model;
    using MA.Service.InitService;
    using Quartz;
    using Quartz.Impl;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EmailSheduler
    {
        public static async void Start(UserWithTasks task)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            DateTime now = Convert.ToDateTime(task.FirstSend);
            JobDataMap m = new JobDataMap();
            m.Put("KeyTask", task);
            int c = 0;
            IJobDetail job = JobBuilder.Create<EmailSender>().UsingJobData(m).Build();
            
            ITrigger trigger = TriggerBuilder.Create() 
                    
                .StartAt(now)                          
                .WithSimpleSchedule(x => x            
                    .WithIntervalInHours(task.Pereodicity)          
                    .RepeatForever())                  
                .Build();                               
            c++;
            await scheduler.ScheduleJob(job, trigger);        
        }
    }
}
