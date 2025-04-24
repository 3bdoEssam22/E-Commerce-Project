using Microsoft.AspNetCore.Mvc;
using ServicesAbstracion;
using Shared.DataTransferObjects.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        //Get Basket
        [HttpGet] //Get BaseURL/api/Basket
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsnyc(id);
            return Ok(basket);
        }

        //Create Or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket([FromBody] BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }


        //Delete Basket
        [HttpDelete("{Key}")] // Delete BaseURL/api/Basket/{Key}
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }


    }
}
