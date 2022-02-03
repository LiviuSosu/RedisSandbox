using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisSandbox.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //https://www.youtube.com/watch?v=188Fy-oCw4w&ab_channel=ProgrammingKnowledge2
        //C:\Program Files\Redis
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IDistributedCache _distributedCache;

        public WeatherForecastController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                var cacheKey = "Livi";
                var existingTime = _distributedCache.GetString(cacheKey);
                if (!string.IsNullOrEmpty(existingTime))
                {
                    var x = "Fetched from cache : " + existingTime;
                }
                else
                {
                    existingTime = "\"cevafffff\"";
                    _distributedCache.SetString(cacheKey, existingTime);
                    var y = "Added to cache : " + existingTime;
                }

             //   var articles = _context.Articles.ToList();
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
