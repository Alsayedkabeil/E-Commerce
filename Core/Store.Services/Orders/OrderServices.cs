using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Domain.Exceptions;
using Store.Services.Abstractions.Orders;
using Store.Services.Spesifications.Orders;
using Store.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Orders
{
    public class OrderServices
        (IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IBasketRepostory _basketRepostory
        ) : IOrderServices
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest order, string userEmail)
        {
            // Implementation for creating an order
            // 1. Get Order Address from order request
            var orderAddress = _mapper.Map<OrderAddress>(order.ShipToAddress);
            // 2. Get Delivery Method from order request
            var deliverMethod = await _unitOfWork.GetRepostory<int, DeliveryMethod>().GetAsync(order.DeliveryMethodId);
            if (deliverMethod is null)
            {
                throw new DeliveryMethodNotFoundException(order.DeliveryMethodId);
            }
            // 3. Get Order Items from Basket Service
            // 3.1 Get Basket By Id
            var basketId = await _basketRepostory.GetBasketByIdAsync(order.BasketId);
            if (basketId is null)
            {
                throw new BasketNotFoundException(order.BasketId);
            }
            // 3.2 Map Basket Items to Order Items
            var orderItems = new List<OrderItem>();
            foreach (var item in basketId.Items)
            {
                // Here you can map each basket item to an order itemi
                // Get Product Details from Product Service if needed
                var product = await _unitOfWork.GetRepostory<int, Product>().GetAsync(item.Id);
                if (product is null)
                {
                    throw new ProductNotFoundException(item.Id);
                }
                if (product.Price != item.Price)
                {
                    item.Price = product.Price; // Update price to current product price
                }
                var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);
                orderItems.Add(orderItem);
                // and add it to a list of order items
            }
            // 4. Calculate Subtotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            // Payment Intent Id can be added here if using a payment service
            var spec = new OrderWithPaymentSpecifications(basketId.PaymentIntentId);
            var existOrder = await _unitOfWork.GetRepostory<Guid, Order>().GetAsync(spec);
            if (existOrder is not null)
            {
                _unitOfWork.GetRepostory<Guid, Order>().Delete(existOrder); // Delete existing order with same payment intent
            }   
            // Create Order
            var newOrder = new Order(userEmail, orderAddress, deliverMethod, orderItems, subTotal,basketId.PaymentIntentId);
            await _unitOfWork.GetRepostory<Guid, Order>().AddAsync(newOrder);
            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0)
            {
                throw new OrderCreateBadException();
            }
            return _mapper.Map<OrderResponse>(newOrder);
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliverMethodsAsync()
        {
            var deliverMethods = await _unitOfWork.GetRepostory<int, DeliveryMethod>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliverMethods);
        }

        public async Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
        {
            var spec = new OrderSpecifications(id, userEmail);
            var orderById = await _unitOfWork.GetRepostory<Guid, Order>().GetAsync(spec);
            return _mapper.Map<OrderResponse>(orderById);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string userEmail)
        {
            var spec = new OrderSpecifications(userEmail);
            var orders = await _unitOfWork.GetRepostory<Guid, Order>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
    }
}
