using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared.Dtos.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } // user email
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now; // order date
        public OrderAddressRequest ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public ICollection<OrderItemRequest> Items { get; set; } // navigation property
        public decimal SubTotal { get; set; } // order subtotal(price * quantity)
        public decimal Total { get; set; } // order total ( subtotal + delivery fee )
        public string? PaymentIntentId { get; set; } // Nullable to allow for no payment intent created
    }
}
