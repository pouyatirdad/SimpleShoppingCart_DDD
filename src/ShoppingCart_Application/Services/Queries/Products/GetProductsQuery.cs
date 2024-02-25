using MediatR;
using ShoppingCart_Application.Responses;
using ShoppingCart_Domain.Entities;
using ShoppingCart_infrastructure.Repositories;

namespace ShoppingCart_Application.Services.Queries.Products
{

    #region query
    public record GetProductsQuery : IRequest<Response<List<Product>>>;

    #endregion
    #region handler
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<List<Product>>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<Product>>();

            var data = await _productRepository.GetAll();

            if (data.Any())
            {
                response.Data = data;
                return response;
            }

            response.Message = "data is empty";
            response.IsSuccess = false;

            return response;
        }
    }
    #endregion
}