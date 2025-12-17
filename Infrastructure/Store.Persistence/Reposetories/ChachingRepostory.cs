using StackExchange.Redis;
using Store.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Persistence.Reposetories
{
    // Implementation of the caching repository using Redis
    public class ChachingRepostory(IConnectionMultiplexer connection) : IChachingRepostory
    {
        private readonly IDatabase _database = connection.GetDatabase(); // Get the Redis database instance
        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key); // Retrieve the value from Redis by key
            return !value.IsNullOrEmpty ? value : default; // Return the value or null if not found
        }

        public async Task SetAsync(string key, object value, TimeSpan time)
        {
            var redisValue = JsonSerializer.Serialize(value); // Serialize the object to JSON format
            await _database.StringSetAsync(key, redisValue, time); // Store the serialized value in Redis with expiration time
        }
    }
}
