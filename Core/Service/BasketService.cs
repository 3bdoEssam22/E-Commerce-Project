using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServicesAbstracion;
using Shared.DataTransferObjects.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    class BasketService(IBasketRepository basketRepository, IMapper _mapper) : IBasketService
    {
        public Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreatedOrUpdatedBasket is not null)
                return GetBasketAsnyc(basket.Id);
            else
                throw new Exception("Can Not Update Or Create Basket Now, Try Again Later.");


        }



        public async Task<BasketDto> GetBasketAsnyc(string Key)
        {
            var Basket = await basketRepository.GetBasketAsync(Key);
            if (Basket is not null)
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            else
                throw new BasketNotFoundException(Key);
        }
        public async Task<bool> DeleteBasketAsync(string Key) => await basketRepository.DeleteBasketAsync(Key);
    }
}
