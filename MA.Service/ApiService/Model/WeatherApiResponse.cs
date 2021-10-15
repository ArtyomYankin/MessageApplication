namespace MA.Service.ApiService
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;



    public partial class Temperatures
    {
        [JsonProperty("data")]
        public List<Datum> Datum { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("rh")]
        public long Rh { get; set; }

        [JsonProperty("pod")]
        public string Pod { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("pres")]
        public long Pres { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("ob_time")]
        public string ObTime { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("clouds")]
        public long Clouds { get; set; }

        [JsonProperty("ts")]
        public long Ts { get; set; }

        [JsonProperty("solar_rad")]
        public long SolarRad { get; set; }

        [JsonProperty("state_code")]
        public long StateCode { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }

        [JsonProperty("wind_spd")]
        public long WindSpd { get; set; }

        [JsonProperty("wind_cdir_full")]
        public string WindCdirFull { get; set; }

        [JsonProperty("wind_cdir")]
        public string WindCdir { get; set; }

        [JsonProperty("slp")]
        public long Slp { get; set; }

        [JsonProperty("vis")]
        public long Vis { get; set; }

        [JsonProperty("h_angle")]
        public long HAngle { get; set; }

        [JsonProperty("sunset")]
        public string Sunset { get; set; }

        [JsonProperty("dni")]
        public long Dni { get; set; }

        [JsonProperty("dewpt")]
        public long Dewpt { get; set; }

        [JsonProperty("snow")]
        public long Snow { get; set; }

        [JsonProperty("uv")]
        public long Uv { get; set; }

        [JsonProperty("precip")]
        public long Precip { get; set; }

        [JsonProperty("wind_dir")]
        public long WindDir { get; set; }

        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }

        [JsonProperty("ghi")]
        public long Ghi { get; set; }

        [JsonProperty("dhi")]
        public long Dhi { get; set; }

        [JsonProperty("aqi")]
        public long Aqi { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("weather")]
        public Weather Weather { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        [JsonProperty("temp")]
        public long Temp { get; set; }

        [JsonProperty("station")]
        public string Station { get; set; }

        [JsonProperty("elev_angle")]
        public double ElevAngle { get; set; }

        [JsonProperty("app_temp")]
        public double AppTemp { get; set; }
    }

    public partial class Weather
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
    


