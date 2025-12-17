using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.Domain.Contracts;
using Store.Domain.Entities.Identity;
using Store.Services.Abstractions;
using Store.Services.Abstractions.Baskets;
using Store.Services.Abstractions.Chaching;
using Store.Services.Abstractions.Orders;
using Store.Services.Abstractions.Payments;
using Store.Services.Abstractions.Products;
using Store.Services.Abstractions.Security;
using Store.Services.Baskets;
using Store.Services.Chaching;
using Store.Services.Orders;
using Store.Services.Payments;
using Store.Services.Products;
using Store.Services.Security;
using Store.Shared.Dtos.OptionsPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IBasketRepostory _basket,
        IChachingRepostory _chaching,
        UserManager<AppUser> _userManager,
        IConfiguration configuration,
        IOptions<JwtOptions> options
        ) : IServiceManager
    {
        public IProductServices ProductServices {get;} = new ProductServices(_unitOfWork,_mapper);

        public IBasketServices BasketServices { get; } = new BasketServices(_basket,_mapper);

        public IChachingServices CachingServices { get; } = new ChachingServices(_chaching);

        public IAuthServices AuthServices { get; } = new AuthServices(_userManager, options,_mapper);

        public IOrderServices OrderServices { get; } = new OrderServices(_unitOfWork, _mapper, _basket);

        public IPaymentServices PaymentServices { get; } = new PaymentServices(_basket, _unitOfWork, configuration, _mapper);
    }
}
