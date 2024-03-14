using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
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
        private readonly IDistributedCache _cacheService;
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator, IDistributedCache cache)
        {
            _mediator = mediator;
            _cacheService = cache ?? throw new ArgumentNullException(nameof(cache));
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
            try
            {
                var cacheData = await _cacheService.GetStringAsync("allProducts");
                if (cacheData != null)
                {
                    return JsonConvert.DeserializeObject<Response<List<Product>>>(cacheData);
                }

                var query = new GetProductsQuery();
                var data = await _mediator.Send(query);

                _cacheService.SetStringAsync("allProducts", JsonConvert.SerializeObject(data));

                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
