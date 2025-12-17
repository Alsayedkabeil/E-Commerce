using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Baskets
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
        public int? DeliveryMethodId { get; set; } // Nullable to allow for no delivery method selected
        public string? PaymentIntentId { get; set; } // Nullable to allow for no payment intent created
        public string? ClientSecret { get; set; } // Nullable to allow for no client secret created
        public decimal? ShippingCost { get; set; } // Nullable to allow for no shipping cost calculated
    }
}
