using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Orders
{
    // Mapping To Table 
    public class Order : BaseEntity<Guid> // PK
    {
        public Order() // Default Constructor
        {
            
        }
        // Parametrized Constructor
        public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string? paymentIntentId)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; } // user email
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now; // order date
        public OrderStatus Status { get; set; } = OrderStatus.Pending; // order status
        public OrderAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } // navigation property
        public int DeliveryMethodId { get; set; } // FK
        public ICollection<OrderItem> Items { get; set; } // navigation property
        public decimal SubTotal { get; set; } // order subtotal(price * quantity)
        //[NotMapped] // not mapped to database
        //public decimal Total { get; set; } // order total ( subtotal + delivery fee )
        public decimal GetTotal() => SubTotal + DeliveryMethod.Price; // order total ( subtotal + delivery fee ) NotMapped
        public string? PaymentIntentId { get; set; } // Nullable to allow for no payment intent created
    }
}
