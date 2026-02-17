using AutoMapper;
using ECommerce.Domain.Entities.BasketModule;
using ECommerce.Domain.Interfaces;
using ECommerce.Services.Abstraction;
using ECommerce.Services.Exceptions;
using ECommerce.SharedLibirary.DTO_s.BasketDTOs;

namespace ECommerce.Services.Servicies
{
    public class BasketService(IBasketRepository repository, IMapper mapper) : IBasketService
    {
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var MappedBasket = mapper.Map<CustomerBasket>(basket);

            var CreatedOrUpdatedBasket = await repository.CreateOrUpdateCustomerBasketAsync(

                MappedBasket
                );


            return mapper.Map<BasketDTO>(CreatedOrUpdatedBasket);

        }

        public async Task<bool> DeleteBasketAsync(string id)
        {

            if(id is null )
            {
                throw new BasketNotFoundException(id);
            }


            return await repository.DeleteBasketAsync(id);

        }


        public async Task<BasketDTO> GetBasketAsync(string id)
        {
            var basket = await repository.GetBasketAsync(id);

            if (basket is null)
            {
                throw new BasketNotFoundException(id);
            }


            var MappedBasket = mapper.Map<BasketDTO>(basket);

            return MappedBasket;
        }
    }
}
