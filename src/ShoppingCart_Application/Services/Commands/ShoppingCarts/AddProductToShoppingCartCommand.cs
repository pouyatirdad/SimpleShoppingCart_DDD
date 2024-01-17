using MediatR;
using ShoppingCart_Application.Responses;
using ShoppingCart_Domain.Entities;
using ShoppingCart_Domain.Events;
using ShoppingCart_infrastructure.Repositories;

namespace ShoppingCart_Application.Services.Commands.ShoppingCarts
{
    #region command
    public record AddProductToShoppingCartCommand : IRequest<Response<ShoppingCart>>
    {
        public Guid ProductId { get; set; }
        public Guid ShoppingCartId { get; set; }
    }
    #endregion
    #region handler
    public class AddProductToShoppingCartCommandHandler : IRequestHandler<AddProductToShoppingCartCommand, Response<ShoppingCart>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;

        public AddProductToShoppingCartCommandHandler(IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Response<ShoppingCart>> Handle(AddProductToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<ShoppingCart>();
            var product = await _productRepository.Get(request.ProductId);
            var shoppingCart = new ShoppingCart(request.ShoppingCartId);

            if (product != null && shoppingCart != null)
            {
                shoppingCart.AddItem(product);

                bool updateCart = await _shoppingCartRepository.Update(shoppingCart);
                if (updateCart)
                {
                    await _shoppingCartRepository.SaveChange();
                    response.Data = shoppingCart;

                    shoppingCart.AddDomainEvent(new AddProductToShoppingCartEvent(shoppingCart));

                    return response;
                }
            }

            response.IsSuccess = false;
            response.Message = "Failed";

            return response;
        }
    }
    #endregion
}
