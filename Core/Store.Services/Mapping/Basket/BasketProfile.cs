using AutoMapper;
using Store.Domain.Entities.Baskets;
using Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Mapping.Basket
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketResponse>().ReverseMap();
            CreateMap<BasketItem, BasketItemResponse>().ReverseMap();
        }
    }
}
