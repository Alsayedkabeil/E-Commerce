using StackExchange.Redis;
using Store.Domain.Contracts;
using Store.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Persistence.Reposetories
{
    public class BasketRepostory(IConnectionMultiplexer connection) : IBasketRepostory
    {
        private readonly IDatabase _database = connection.GetDatabase(); // Get the Redis database instance
        public async Task<CustomerBasket?> GetBasketByIdAsync(string id)
        {
            var redisValue = await _database.StringGetAsync(id);
            if (redisValue.IsNullOrEmpty) // Check if the value is null or empty
            {
                return null;
            }
            // Deserialize the redisValue(Json) to CustomerBasket and return
            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);  // Deserialize the JSON string to CustomerBasket object
            if (basket is null)
            {
                return null;
            }
            return basket;
        }

        public async Task<CustomerBasket?> UpdateBasketByAsync(CustomerBasket basket, TimeSpan? time = null)
        {
            // Serialize the CustomerBasket object to JSON string and store it in Redis with an expiration time of 30 days
            var redisValue = JsonSerializer.Serialize(basket);
            var flag = await _database.StringSetAsync(basket.Id, redisValue, TimeSpan.FromDays(30));
            return flag ? await GetBasketByIdAsync(basket.Id) : null; // If the operation is successful, retrieve and return the updated basket
        }

        public async Task<bool> DeleteBasketByIdAsync(string id)
        {
            return await _database.KeyDeleteAsync(id); // Delete the basket by its ID and return the result
        }

       
    }
}
