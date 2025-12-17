using Store.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Spesifications.Orders
{
    public class OrderSpecifications : BaseSpesifications<Guid, Order>
    {
        public OrderSpecifications(Guid id, string userEmail)
            : base(O => O.Id == id && O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }

        public OrderSpecifications(string userEmail)
           : base(O => O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }

    }
}
