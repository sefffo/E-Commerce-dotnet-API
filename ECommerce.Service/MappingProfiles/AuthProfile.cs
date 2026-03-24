using AutoMapper;
using ECommerce.Domain.Entities.IdentityModule;
using ECommerce.SharedLibirary.DTO_s.OrderDTOs;

namespace ECommerce.Services.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AddressDTO, Address>();
            CreateMap<Address, AddressDTO>();
        }

    }
}
