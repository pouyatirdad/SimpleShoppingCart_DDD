using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart_Application.Common.Interfaces;
using ShoppingCart_Application.Responses;
using ShoppingCart_Application.Services.Commands.Products;
using ShoppingCart_Application.Services.Queries.Products;
using ShoppingCart_Domain.Entities;

namespace ShoppingCart_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator, ICacheService cacheService)
        {
            _mediator = mediator;
            _cacheService = cacheService;
        }
        [HttpPost("CreateProduct")]
        public async Task<Response<Product>> CreateProduct([FromBody] CreateProductCommand product)
        {
            var data = await _mediator.Send(product);

            return data;
        }
        [HttpGet("GetAll")]
        public async Task<Response<List<Product>>> GetAllProducts()
        {
            var cacheData = _cacheService.GetData<Response<List<Product>>>("allProducts");
            if (cacheData != null)
            {
                return cacheData;
            }
            
            var query = new GetProductsQuery();
            var data = await _mediator.Send(query);

            var expirationTime = DateTimeOffset.Now.AddHours(12);
            _cacheService.SetData<Response<List<Product>>>("allProducts", data, expirationTime);

            return data;
        }
    }
}
