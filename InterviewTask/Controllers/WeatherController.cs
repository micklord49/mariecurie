using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using InterviewTask.Services;
using InterviewTask.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace InterviewTask.Controllers
{
    public class WeatherController : Controller
    {
        public class Clouds
        {
            public int all { get; set; }
        }

        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public int sea_level { get; set; }
            public int grnd_level { get; set; }
        }

        public class Rain
        {
            public double _1h { get; set; }
        }

        public class WeatherRoot
        {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public int visibility { get; set; }
            public Wind wind { get; set; }
            public Rain rain { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public int timezone { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }

        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
            public int deg { get; set; }
            public double gust { get; set; }
        }

        public class WeatherError
        {
            public string cod { get; set; }
            public string message { get; set; }
        }
    


        // POST: Weather
        [HttpPost]
        public ActionResult GetWeather(Guid Id)
        {
            FileLogger log = new FileLogger(Server.MapPath("~/log.txt"));
            HelperServiceRepository repository = new HelperServiceRepository();

            var service = repository.Get(Id);
            WeatherRoot weather = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={service.Lat}&lon={service.Lon}&appid=fb92cfdfb3c022de6144ae9743100ef0");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("").Result;  
            if (response.IsSuccessStatusCode)
            {
                weather = JsonConvert.DeserializeObject<WeatherRoot>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                log.Log($"WEATHER ERROR RESPONSE: {response.StatusCode}-{response.ReasonPhrase}");
                return PartialView("_WeatherError", new WeatherError() { cod = response.StatusCode.ToString(), message = response.ReasonPhrase });
            }
            return PartialView("_Weather", weather);
        }
    }
}