using AutoMapper;
using E_Commerce.Domain.Entities.BasketModule;
using E_Commerce.Domain.Intreface;
using E_Commerce.Services.Exceptions;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = E_Commerce.Shared.CommonResult.Error;

namespace E_Commerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketService( IBasketRepository _basketRepository , IMapper _mapper )
        {
            basketRepository = _basketRepository;
            mapper = _mapper;
        }

        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            // CustomerBasket الي  BasketDTO هتحول من
            var customerBasket = mapper.Map<BasketDTO , CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            // BasketDTO الي  CustomerBasket هتحول من
            return mapper.Map<CustomerBasket, BasketDTO>(CreatedOrUpdatedBasket!);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
           return await basketRepository.DeleteBasketAsync(id);
        }

        public async Task<Result<BasketDTO>> GetBasketAsync(string id)
        {
           var Basket = await basketRepository.GetBasketAsync(id);
            if (Basket is null)
                return Error.NotFound(" Basket.NotFound", $" Basket With Id {id} Is Not Found");
            return mapper.Map<CustomerBasket , BasketDTO>(Basket);

        }
    }
}
