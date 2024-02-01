using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if(String.IsNullOrEmpty(basket)) return null;

#pragma warning disable CS8603 // Possível retorno de referência nula.
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
#pragma warning restore CS8603 // Possível retorno de referência nula.

        }


        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
#pragma warning disable CS8604 // Possível argumento de referência nula.
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
#pragma warning restore CS8604 // Possível argumento de referência nula.

            return await GetBasket(basket.UserName);
        }
    
        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}