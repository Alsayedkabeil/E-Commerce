using AutoMapper;
using Store.Domain.Entities.Identity;
using Store.Shared.Dtos.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Mapping.Security
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Address, AddressResponse>().ReverseMap(); // Map Address entity to AddressResponse DTO
        }
    }
}
