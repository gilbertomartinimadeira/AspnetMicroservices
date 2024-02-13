using System.Net;
using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories;
using EventBus.Messages.Events;

//using Discount.Grpc.Protos.Client;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;    
        private readonly IMapper _mapper;
        //private readonly DiscountProtoService.DiscountProtoServiceClient _client;


        // public BasketController(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient client)
        // {
        //     _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        //     //_client = client;
        // }

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
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
            // foreach (var item in basket.Items)
            // {

            //     var coupon =  _client.GetDiscount(new GetDiscountRequest{ ProductName= item.ProductName} );
            //     item.Price -= coupon.Amount;
            // }
            
            return Ok(await _basketRepository.UpdateBasket(basket));
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _basketRepository.DeleteBasket(username);
            return Ok();
        }


        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {


            //TODO: Get Existing basket with total price
            var basket = await _basketRepository.GetBasket(basketCheckout.UserName);

            if(basket == null) return NotFound();

            //TOOD: Create BasketCheckoutEvent -- Set TotalPrice on basketcheckout eventmessage
            

            //TODO: Send Checkout event to rabbitmq


            //TODO: remove the basket
            await _basketRepository.DeleteBasket(basket.UserName);


            return Accepted();

        }



    }
}