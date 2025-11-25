using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.BasketDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService basketService;

        public BasketsController( IBasketService basketService ) 
        {
            this.basketService = basketService;
        }

        // Get : BaseUrl /api/Baskets?id
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasket(string id)
        {
            var Basket = await basketService.GetBasketAsync(id);
            return Ok(Basket);
        }
        // POST : // BaseUrl/api/Baskets 
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basket)
        {
            var Basket = await basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        // Delete : BaseUrl/api/Baskets/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            var Result = await basketService.DeleteBasketAsync(id);
            return Ok(Result);
        }
    }
}
