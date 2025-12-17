using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation
{
    // API controller for managing baskets
    // Route: /api/baskets
    // Inherits from ControllerBase to provide API functionalities
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager _serviceManager) : ControllerBase
    {
        #region GetBasketById
        [HttpGet] // GET: /api/baskets?id={id}
        public async Task<IActionResult> GetBasket(string id)
        {
            var result = await _serviceManager.BasketServices.GetBasketByAsync(id); // Call the service to get the basket by ID
            return Ok(result); // Return 200 OK with the basket details
        }
        #endregion

        #region Create Or Update Basket
        [HttpPost] // POST: /api/baskets
        public async Task<IActionResult> CreateOrUpdateBasket(BasketResponse basket)
        {
            var result = await _serviceManager.BasketServices.UpdateBasketByAsync(basket);
            return Ok(result); // Return 200 OK with the updated basket details
        }
        #endregion

        #region Delete Basket
        [HttpDelete] // DELETE: /api/baskets?id={id}
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _serviceManager.BasketServices.DeleteBasketByAsync(id); // Call the service to delete the basket by ID
            return NoContent(); // Return 204 No Content to indicate successful deletion
        }
        #endregion
    }
}
