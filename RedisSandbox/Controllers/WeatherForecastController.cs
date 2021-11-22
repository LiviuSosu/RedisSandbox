using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

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

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDistributedCache _distributedCache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDistributedCache distributedCache)
        {
            _logger = logger;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                //pinger = new Ping();
                //PingReply reply = pinger.Send("redis-cli ping");
                //pingable = reply.Status == IPStatus.Success;


                //System.Diagnostics.Process process = new System.Diagnostics.Process();
                //process.StartInfo.UseShellExecute = false;
                //process.StartInfo.RedirectStandardOutput = true;
                //// process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //process.StartInfo.WorkingDirectory = @"C:\Program Files\Redis";

                //process.StartInfo.FileName = "C:\\Windows\\System32\\cmd.exe";
                //process.StartInfo.Arguments = "redis-cli ping";
                //process.Start();

                //while (!process.StandardOutput.EndOfStream)
                //{
                //    var line = process.StandardOutput.ReadLine();
                //    Console.WriteLine(line);
                //}

                //process.WaitForExit();


                //ProcessStartInfo startInfo = new ProcessStartInfo();
                //startInfo.WorkingDirectory = @"C:\Program Files\Redis\";
                //startInfo.FileName = @"C:\Program Files\Redis\redis-server.exe";
                ////startInfo.Arguments = f;
                //System.Diagnostics.Process process = new System.Diagnostics.Process();
                //Process.Start(startInfo);

                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");


            }
            catch (Exception ex)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }


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
