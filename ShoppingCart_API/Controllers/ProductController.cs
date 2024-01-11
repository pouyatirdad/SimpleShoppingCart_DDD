using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_Application.Responses;
using ShoppingCart_Application.Services.Commands.Products;
using ShoppingCart_Domain.Entities;

namespace ShoppingCart_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateProduct")]
        public async Task<Response<Product>> CreateProduct([FromBody] AddProductCommand product)
        {
            var data = await _mediator.Send(product);

            return data;
        }
    }
}
