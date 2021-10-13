namespace MessageApplication.EmailSender
{
    using MA.Data.Model;
    using Quartz;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailSender: IJob
    {
        private readonly string Email = Environment.GetEnvironmentVariable("App_Email");
        private readonly string Pass = Environment.GetEnvironmentVariable("qwerty2002.");

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            UserWithTasks class1 = (UserWithTasks)dataMap.Get("KeyTask");
            using (MailMessage message = new MailMessage(Email, class1.Email))
            {
                message.Subject = "Новостная рассылка";
                message.Body = "Новости сайта: бла бла бла";
                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = true,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential(Email, Pass)
                })
                {
                    await client.SendMailAsync(message);
                }
            }
        }
    }
}
