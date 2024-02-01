using MediatR;
using ShoppingCart_Application.Responses;
using ShoppingCart_Domain.Entities;
using ShoppingCart_infrastructure.Repositories;

namespace ShoppingCart_Application.Services.Queries.ShoppingCarts
{
    #region query
    public record GetShoppingCartsQuery : IRequest<Response<List<ShoppingCart>>>;

    #endregion
    #region handler
    public class GetShoppingCartsQueryHandler : IRequestHandler<GetShoppingCartsQuery, Response<List<ShoppingCart>>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public GetShoppingCartsQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<Response<List<ShoppingCart>>> Handle(GetShoppingCartsQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ShoppingCart>>();

            var data = await _shoppingCartRepository.GetAll();

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
