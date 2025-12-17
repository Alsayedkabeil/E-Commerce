using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared.Dtos.Baskets
{
    public class BasketResponse
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemResponse> Items { get; set; }
        public int? DeliveryMethodId { get; set; } // Nullable to allow for no delivery method selected
        public string? PaymentIntentId { get; set; } // Nullable to allow for no payment intent created
        public string? ClientSecret { get; set; } // Nullable to allow for no client secret created
        public decimal? ShippingCost { get; set; } // Nullable to allow for no shipping cost calculated
    }
}
