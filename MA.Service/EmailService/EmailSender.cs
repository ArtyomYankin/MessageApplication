using CsvHelper;
using MA.Data.Model;
using MA.Service.ApiService;
using MA.Service.ApiService.Model;
using Newtonsoft.Json;
using Quartz;
using RestSharp;
using System;
    using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
using static MA.Service.ApiService.LastFmApiResponse;

namespace MessageApplication.EmailSender
{
    public class EmailSender: IJob
    {
        string email = Environment.GetEnvironmentVariable("App_Email");
        string password = Environment.GetEnvironmentVariable("App_Email_Password");
        string lastFmApiKey = Environment.GetEnvironmentVariable("LastFmApiKey");
        string filePath = Environment.GetEnvironmentVariable("File_Path");
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            UserWithTasks class1 = (UserWithTasks)dataMap.Get("KeyTask");
            string filePath = null;
            switch (class1.ApiType)
            {
                case "TopTrack":
                    filePath = await GetTopTracksForArtist(class1.ApiParam, class1.Id);
                    break;
                case "Weather":
                    filePath = await GetWeather(class1.ApiParam, class1.Id);
                    break;
                case "Covid":
                    filePath = await GetCovidStats(class1.ApiParam, class1.Id);
                    break;
                default:
                    break;
            }

            using (MailMessage message = new MailMessage(email, class1.Email))
            {
                message.Subject = "Рассылка информации.";
                message.Attachments.Add(new Attachment(filePath));
                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = true,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential(email, password)
                })
                {
                    await client.SendMailAsync(message);
                }
            }
            File.Delete(filePath);
        }
        public async Task<string> GetWeather(string apiParam, int id)
        {
            var client = new RestClient("https://api.weatherbit.io/v2.0/current?city=" + $"{apiParam}" + "&key=71f97697937e437f910f61fffd567315");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var temperatures = JsonConvert.DeserializeObject<Temperatures>(response.Content);
                var temp = temperatures.Datum;
                foreach (var t in temp)
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    {
                        using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                        {
                            csv.WriteHeader<Datum>();
                            csv.NextRecord();
                            csv.WriteRecord(t);
                        }
                    }
                    return filePath;
                }
            }
            return null;
        }
        public async Task<string> GetTopTracksForArtist(string apiParam, int id)
        {
            
            var client = new RestClient("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist="+$"{apiParam}"+$"&api_key={lastFmApiKey}"+"&limit=1&format=json");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var expandos = JsonConvert.DeserializeObject<LastFmApi>(response.Content);
                var tracks = expandos.Toptracks.Track;
                foreach (var track in tracks)
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    {
                        using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                        {
                            csv.WriteHeader<Track>();
                            csv.NextRecord();
                            csv.WriteRecord(track);
                        }
                    }
                }
                return filePath;
            }
            return null;
        }
        public async Task<string> GetCovidStats(string apiParam, int id)
        {
            
            var client = new RestClient("https://covid19-api.com/country?name="+$"{apiParam}"+"&format=json");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var expandos = JsonConvert.DeserializeObject<List<Covid>>(response.Content);
                foreach (var n in expandos)
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    {
                        using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                        {
                            csv.WriteHeader<Covid>();
                            csv.NextRecord();
                            csv.WriteRecord(n);
                        }
                    }
                }
                return filePath;
            }
            return null;
        }
    }
   
}
