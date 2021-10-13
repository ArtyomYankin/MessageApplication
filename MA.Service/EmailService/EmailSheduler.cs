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
            
            JobDataMap m = new JobDataMap();
            m.Put("KeyTask", task);
            int c = 0;
            IJobDetail job = JobBuilder.Create<EmailSender>().UsingJobData(m).Build();
            
            ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                    // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                    .WithIntervalInHours(1)          // через 1 минуту
                    .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер
            c++;
            await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
        }
    }
}
