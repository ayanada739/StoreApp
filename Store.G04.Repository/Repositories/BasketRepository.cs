 using StackExchange.Redis;
using Store.G04.Core.Entities;
using Store.G04.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G04.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
            
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
           return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustumerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);

            return basket.IsNullOrEmpty ? null: JsonSerializer.Deserialize<CustumerBasket>(basket);
        }

        public async Task<CustumerBasket?> UpdateBasketAsync(CustumerBasket basket)
        {
           var CreatedOrUpdatedBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize<CustumerBasket>(basket), TimeSpan.FromDays(30));

            if (CreatedOrUpdatedBasket is false) return null;
            return await GetBasketAsync(basket.Id);

        }
    }
}
