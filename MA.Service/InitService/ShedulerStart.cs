namespace MA.Service.InitService
{
    using Quartz;
    using Quartz.Impl;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ShedulerStart
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<Initializer>().Build();

            ITrigger trigger = TriggerBuilder.Create()     
                .StartNow()
                .WithSimpleSchedule(x => x
                               .WithIntervalInHours(1)
                               .RepeatForever())
                .Build();                              

            await scheduler.ScheduleJob(job, trigger);       
        }
    }
}
