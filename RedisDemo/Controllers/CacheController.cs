using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisDemo.Contracts;
using RedisDemo.Services;

namespace RedisDemo.Controllers
{
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("cache/{key}")]
        public async Task<IActionResult> GetCacheValue([FromRoute] string key)
        {
            var value = await _cacheService.GetCacheValueAsync(key);
            return string.IsNullOrEmpty(value) ? (IActionResult)NotFound() : Ok(value);
        }

        [HttpPost("cache")]
        public async Task<IActionResult> SetCacheValue([FromBody] NewCacheEntryRequest request)
        {
            await _cacheService.SetCacheValueAsync(request.Key, request.Value);
            return Ok();
        }
    }
}
