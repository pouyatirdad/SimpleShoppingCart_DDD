using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingCart_Application.Responses;
using ShoppingCart_Domain.Entities;
using Newtonsoft.Json;
using ShoppingCart_infrastructure.Repositories;

namespace ShoppingCart_Application.Services.Queries.Products;

#region query
public record GetProductsQuery : IRequest<Response<List<Product>>>;

#endregion
#region handler
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<List<Product>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IDistributedCache _cacheService;

    public GetProductsQueryHandler(IProductRepository productRepository, IDistributedCache cacheService)
    {
        _productRepository = productRepository;
        _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    }

    public async Task<Response<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<List<Product>>();

        var cacheData = await _cacheService.GetStringAsync("allProducts");
        if (cacheData != null)
        {
            response.Data = JsonConvert.DeserializeObject<List<Product>>(cacheData);
            return response;
        }

        var data = await _productRepository.GetAll();

        if (data.Any())
        {
            await _cacheService.SetStringAsync("allProducts", JsonConvert.SerializeObject(data));

            response.Data = data;
            return response;
        }

        response.Message = "data is empty";
        response.IsSuccess = false;

        return response;
    }
}
#endregion
