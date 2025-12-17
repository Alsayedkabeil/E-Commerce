using Store.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface IBasketRepostory
    {
        // Define methods for basket repository
        // Update == Create if not exists
        Task<CustomerBasket?> GetBasketByIdAsync(string id); // Get basket by id
        Task<CustomerBasket?> UpdateBasketByAsync(CustomerBasket basket, TimeSpan? time = null); // Update or create basket
        Task<bool> DeleteBasketByIdAsync(string id); // Delete basket by id
    }
}
