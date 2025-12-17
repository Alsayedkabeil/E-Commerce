using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using Store.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation
{
    // API controller for managing orders
    // Route: /api/orders
    // Inherits from ControllerBase to provide API functionalities
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager _serviceManager) : ControllerBase
    {
        #region Create Order
        [HttpPost] // POST: api/orders
        [Authorize] // Requires authentication
        public async Task<IActionResult> CreateOrder(OrderRequest order)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.OrderServices.CreateOrderAsync(order, userEmail.Value);
            return Ok(result);
        }
        #endregion

        #region GetAllDeliverMethods
        [HttpGet("deliveryMethods")] // GET: api/orders/deliveryMethods
        public async Task<IActionResult> GetAllDeliverMethods()
        {
            var result = await _serviceManager.OrderServices.GetAllDeliverMethodsAsync();
            return Ok(result);
        }
        #endregion

        #region GetOrderByIdForSpecificUserAsync
        [HttpGet("{id:Guid}")] // GET: api/orders/{id}
        [Authorize] // Requires authentication
        public async Task<IActionResult> GetOrderByIdForSpecificUser(Guid id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _serviceManager.OrderServices.GetOrderByIdForSpecificUserAsync(id, userEmail);
            return Ok(result);
        }
        #endregion

        #region GetOrdersForSpecificUserAsync
        [HttpGet] // GET: api/orders
        [Authorize]
        public async Task<IActionResult> GetOrders()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _serviceManager.OrderServices.GetOrdersForSpecificUserAsync(userEmail);
            return Ok(result);
        }
        #endregion
    }
}
