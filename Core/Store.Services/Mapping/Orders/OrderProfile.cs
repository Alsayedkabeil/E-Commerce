using AutoMapper;
using Store.Domain.Entities.Orders;
using Store.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()));
            CreateMap<OrderAddress, OrderAddressRequest>().ReverseMap();
            CreateMap<OrderItem, OrderItemRequest>()
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom(S => S.Product.PictureUrl));
            CreateMap<DeliveryMethod, DeliveryMethodResponse>();
        }
    }
}
