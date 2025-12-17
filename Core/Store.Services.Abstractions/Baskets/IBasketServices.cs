using Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions.Baskets
{
    public interface IBasketServices
    {
        Task<BasketResponse?> GetBasketByAsync(string id);
        Task<BasketResponse?> UpdateBasketByAsync(BasketResponse basket);
        Task<bool> DeleteBasketByAsync(string id);
    }
}
