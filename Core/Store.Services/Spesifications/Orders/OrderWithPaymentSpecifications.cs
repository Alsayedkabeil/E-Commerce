using Store.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Spesifications.Orders
{
    public class OrderWithPaymentSpecifications : BaseSpesifications<Guid, Order>
    {
        public OrderWithPaymentSpecifications(string paymentIntentId) 
            : base(O => O.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
