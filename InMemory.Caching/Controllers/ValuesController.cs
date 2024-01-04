using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<IActionResult> Set(string key = "name",string value = null)
        {
            if (string.IsNullOrEmpty(value))
                return BadRequest("Value is not null");
            
            _memoryCache.Set(key, value);

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string key)
        {
            if(_memoryCache.TryGetValue<string>(key, out string name))
                return Ok(name);

            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string key)
        {
            _memoryCache.Remove(key);

            return Ok();    
        }


        [HttpPost("SetDate")]
        public async Task<IActionResult> SetDate()
        {
            _memoryCache.Set<DateTime?>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(15),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });

            return Created();
        }

        [HttpGet("Getdate")]
        public async Task<IActionResult> Getdate()
        {
            if (_memoryCache.TryGetValue<DateTime>("date", out DateTime date))
                return Ok(date);

            return NotFound();
        }

    }
}
