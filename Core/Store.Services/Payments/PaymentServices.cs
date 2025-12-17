using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Domain.Contracts;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Domain.Exceptions;
using Store.Services.Abstractions.Payments;
using Store.Shared.Dtos.Baskets;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Domain.Entities.Products.Product;

namespace Store.Services.Payments
{
    public class PaymentServices
        (IBasketRepostory _basketRepostory,
        IUnitOfWork _unitOfWork,
        IConfiguration configuration,
        IMapper _mapper
        ) : IPaymentServices
    {
        public async Task<BasketResponse> CreatePaymentIntentAsync(string basketId)
        {
            // Get the basket
            var basket = await _basketRepostory.GetBasketByIdAsync(basketId);
            if (basket is null)
            {
                throw new BasketNotFoundException(basketId);
            }
            // Update prices of items in the basket
            foreach (var item in basket.Items)
            {
                // Here you would typically fetch the latest price from your product catalog
                // For simplicity, let's assume the price is already correct in the basket
                var product = await _unitOfWork.GetRepostory<int,Product>().GetAsync(item.Id);
                if (product is null)
                {
                    throw new ProductNotFoundException(item.Id);
                }
                item.Price = product.Price;
            }

            // Calculate Amount = Subtotal + DeliveryFee
            var subTotal = basket.Items.Sum(item => item.Price * item.Quantity);

            // Get delivery fee if delivery method is selected
            if (!basket.DeliveryMethodId.HasValue)
            {
                throw new DeliveryMethodNotFoundException(-1); // You might want to handle this differently
            }
            var deliveryMethod = await _unitOfWork.GetRepostory<int, DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
            if (deliveryMethod is null)
            {
                throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            }
            basket.ShippingCost = deliveryMethod.Price; // Update shipping cost in the basket
            var amount = subTotal + deliveryMethod.Price; // Total amount in the smallest currency unit

            // Send request to Stripe API to create PaymentIntent
            StripeConfiguration.ApiKey = configuration["Stripe:Secretkey"]; // Your Stripe secret key from configuration
            PaymentIntentService paymentIntentService = new PaymentIntentService(); // Create an instance of the PaymentIntentService
            PaymentIntent paymentIntent; // Declare the PaymentIntent variable

            if (basket.PaymentIntentId is null)
            {
                // Create new PaymentIntent
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(amount * 100), // Amount in cents
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" },
                };
                paymentIntent = await paymentIntentService.CreateAsync(options); // Create the PaymentIntent using the service
            }
            else
            {
                // Update existing PaymentIntent
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(amount * 100), // Amount in cents
                };
                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options); // Create the PaymentIntent using the service
            }
            // Update basket with PaymentIntent details
            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;
            basket = await _basketRepostory.UpdateBasketByAsync(basket, TimeSpan.FromDays(1)); // Update the basket with a 1-day expiration
            return _mapper.Map<BasketResponse>(basket);
        }
    }
}
