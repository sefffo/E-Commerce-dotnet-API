using AutoMapper;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.SharedLibirary.DTO_s.OrderDTOs;

namespace ECommerce.Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO, OrderShippingAddress>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductName, src => src.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, src => src.MapFrom<OrderProductPictureUrlResolver>());

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName));


            CreateMap<DeliveryMethod, DeliveryMethodDTO>();
        }
    }
}
