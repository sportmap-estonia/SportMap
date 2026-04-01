using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SportMap.AL.Abstractions.Services;
using StackExchange.Redis;

namespace SportMap.DAL.Cache
{
    public sealed class RedisCacheService(IConnectionMultiplexer connectionMultiplexer, ILogger<RedisCacheService> logger) : ICacheService
    {
        private readonly ILogger _logger = logger;
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

        public bool ExistsAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var exists = _database.KeyExists(key);
                return exists;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{redisService}.{methodName}: Error while checking the existence of a value in cache: {message}", nameof(RedisCacheService), nameof(ExistsAsync), e.Message);
                return false;
            }
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var value = await _database.StringGetAsync(key);

                if (value.HasValue)
                {
                    var json = value.ToString();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{redisService}.{methodName}: Error while getting value to cache: {message}", nameof(RedisCacheService), nameof(GetAsync), e.Message);
                return default;
            }

            return default;
        }

        public Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken cancellationToken = default)
        {
            try
            {
                var json = JsonConvert.SerializeObject(value);
                return _database.StringSetAsync(key, json, ttl);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{redisService}.{methodName}: Error while setting value to cache: {key}", nameof(RedisCacheService), nameof(SetAsync), key);
                return Task.FromException(e);
            }
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                return _database.KeyDeleteAsync(key);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{redisService}.{methodName}: Error while removing value from cache: {key}", nameof(RedisCacheService), nameof(SetAsync), key);
                return Task.FromException(e);
            }
        }
    }
}
