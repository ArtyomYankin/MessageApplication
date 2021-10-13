using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MA.Service.ApiService
{


    public class ApiService : IApiService
    {
        public async Task<Main> GetWeatherAsync()
        {
            var client = new RestClient("https://api.openweathermap.org/data/2.5/weather?q=Moscow&appid=c99f17d54c3cd4b7ecd31942b2e13650");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                Main main = JsonConvert.DeserializeObject<Main>(response.Content);
                return main ;
            }
            return null;
        }
    }
}
