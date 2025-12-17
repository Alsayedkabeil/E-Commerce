using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation
{
    // API controller for handling payment-related requests
    // Route is set to "api/payments"
    // Inherits from ControllerBase to provide basic API functionalities.
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IServiceManager _serviceManager) : ControllerBase
    {

        #region Create PaymentIntent
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            var result = await _serviceManager.PaymentServices.CreatePaymentIntentAsync(basketId);
            return Ok(result);
        }
        #endregion
    }
}
