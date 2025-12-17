using Store.Services.Abstractions.Baskets;
using Store.Services.Abstractions.Chaching;
using Store.Services.Abstractions.Orders;
using Store.Services.Abstractions.Payments;
using Store.Services.Abstractions.Products;
using Store.Services.Abstractions.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions
{
    public interface IServiceManager
    {
        IProductServices ProductServices { get; }
        IBasketServices BasketServices { get; }
        IChachingServices CachingServices { get; }
        IAuthServices AuthServices { get; }
        IOrderServices OrderServices { get; }
        IPaymentServices PaymentServices { get; }
    }
}
