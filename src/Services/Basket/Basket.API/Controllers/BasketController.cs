using System.Net;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {

        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGprcService;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGprcService)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _discountGprcService = discountGprcService ?? throw new ArgumentNullException(nameof(discountGprcService));
        }

        [HttpGet("{username}", Name="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            var basket = await _basketRepository.GetBasket(username);

            if(basket == null) return NoContent();
            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //TODO: Communicate with Discount.Grpc 
            // Calculate latest prices for products inside the shopping cart
            // consume Discount Gppc
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGprcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
        
            return Ok(await _basketRepository.UpdateBasket(basket));
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _basketRepository.DeleteBasket(username);
            return Ok();
        }



    }
}