using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Baskets;
using Store.Domain.Exceptions;
using Store.Services.Abstractions.Baskets;
using Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Baskets
{
    public class BasketServices(IBasketRepostory basketRepostory,IMapper mapper) : IBasketServices
    {
        // Implement the methods defined in the IBasketServices interface
        public async Task<BasketResponse?> GetBasketByAsync(string id)
        {
            var basket = await basketRepostory.GetBasketByIdAsync(id);
            if (basket is null)
            {
                throw new BasketNotFoundException(id);
            }
            var result = mapper.Map<BasketResponse>(basket);
            return result;
        }

        public async Task<BasketResponse?> UpdateBasketByAsync(BasketResponse basket)
        {
            var basketmap = mapper.Map<CustomerBasket>(basket); // Map BasketResponse to CustomerBasket
            basketmap = await basketRepostory.UpdateBasketByAsync(basketmap); // Update the basket in the repository
            if (basketmap is null)
            {
                throw new BasketCreateOrUpdateBadException();
            }
            return mapper.Map<BasketResponse>(basketmap); // Map back to BasketResponse and return

        }

        public async Task<bool> DeleteBasketByAsync(string id)
        {
            var basket = await basketRepostory.DeleteBasketByIdAsync(id);
            if (basket is false)
            {
                throw new BasketDeleteBadException();
            }
            return basket;
        }


    }
}
