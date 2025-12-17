using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Domain.Entities.Products;
using Store.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Mapping.products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductResponse>()
            .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name))
            .ForMember(D => D.Type, O => O.MapFrom(S => S.Type.Name))
            /*.ForMember(D => D.PictureUrl, O => O.MapFrom(S => $"{configuration["BaseUrl"]}/{S.PictureUrl}"));*/ // Adjust the base URL as needed 
            .ForMember(D => D.PictureUrl, O => O.MapFrom(new ProductPictureUrl(configuration))); // Using custom value resolver for PictureUrl

            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();
        }
    }
}
