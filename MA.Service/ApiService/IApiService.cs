namespace MA.Service.ApiService
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IApiService
    {
        Task<Main> GetWeatherAsync();
    }
}
