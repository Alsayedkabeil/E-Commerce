using Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions.Payments
{
    public interface IPaymentServices
    {
        Task<BasketResponse> CreatePaymentIntentAsync(string basketId); // Create a payment intent for the given basket ID
    }
}
