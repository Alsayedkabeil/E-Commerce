using Store.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions.Orders
{
    // Signature for order-related services
    public interface IOrderServices
    {
        Task<OrderResponse?> CreateOrderAsync(OrderRequest order, string userEmail); // [Authenticated+ userEmail From Token]
        Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliverMethodsAsync();
        Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail); // [Authenticated+ userEmail From Token]
        Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string userEmail); // [Authenticated+ userEmail From Token]
    }
}
