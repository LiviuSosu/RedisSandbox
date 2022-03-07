using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace RedisSandbox.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SomeController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public SomeController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public void Get()
        {
            try
            {
                //https://www.youtube.com/watch?v=188Fy-oCw4w&ab_channel=ProgrammingKnowledge2
                //C:\Program Files\Redis
                //https://github.com/microsoftarchive/redis/releases
                var cacheKey = "MyRedisKEy";
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
            }
            catch (RedisConnectionException ex)
            {

            }
            catch (Exception ex)
            {
            }
        }
    }
}