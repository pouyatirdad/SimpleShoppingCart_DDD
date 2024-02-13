using MediatR;
using ShoppingCart_Application.Responses;
using ShoppingCart_Domain.Entities;
using ShoppingCart_infrastructure.Repositories;

namespace ShoppingCart_Application.Services.Queries.ShoppingCarts
{
    #region query
    public record GetShoppingCartByIdQuery : IRequest<Response<ShoppingCart>>
    {
        public Guid ShoppingCartId { get; set; }
    };

    #endregion
    #region handler
    public class GetShoppingCartByIdQueryHandler : IRequestHandler<GetShoppingCartByIdQuery, Response<ShoppingCart>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public GetShoppingCartByIdQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<Response<ShoppingCart>> Handle(GetShoppingCartByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<ShoppingCart>();

            var data = await _shoppingCartRepository.Get(request.ShoppingCartId);

            if (data != null)
            {
                response.Data = data;
                return response;
            }

            response.Message = "there is no shoppingCart";
            response.IsSuccess = false;

            return response;
        }
    }
    #endregion
}
