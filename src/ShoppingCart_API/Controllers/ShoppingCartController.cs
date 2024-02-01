using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_Application.Responses;
using ShoppingCart_Application.Services.Commands.ShoppingCarts;
using ShoppingCart_Application.Services.Queries.ShoppingCarts;
using ShoppingCart_Domain.Entities;

namespace ShoppingCart_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetShoppingCarts")]
        public async Task<Response<List<ShoppingCart>>> GetShoppingCarts()
        {
            var query =new GetShoppingCartsQuery();
            var data = await _mediator.Send(query);

            return data;
        }
        [HttpPost("CreateShoppingCart")]
        public async Task<Response<Guid>> CreateShoppingCart([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            var data = await _mediator.Send(createShoppingCartCommand);

            return data;
        }
        [HttpPost("AddProductToShoppingCartCommand")]
        public async Task<Response<ShoppingCart>> AddProductToShoppingCartCommand([FromBody] AddProductToShoppingCartCommand addProductToShoppingCartCommand)
        {
            var data = await _mediator.Send(addProductToShoppingCartCommand);

            return data;
        }
    }
}
