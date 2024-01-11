using MediatR;
using ShoppingCart_Application.Responses;
using ShoppingCart_Domain.Entities;
using ShoppingCart_Domain.Events;
using ShoppingCart_infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart_Application.Services.Commands.ShoppingCarts
{
    public class AddProductToShoppingCartCommand:IRequest<Response<ShoppingCart>>
    {
        public Guid ProductId { get; set; }
        public Guid ShoppingCartId { get; set; }
    }
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
            var product = _productRepository.Get(request.ProductId);
            var shoppingCart = _shoppingCartRepository.Get(request.ShoppingCartId);

            if (product.Result != null && shoppingCart.Result != null)
            {
                shoppingCart.Result.AddItem(product.Result);

                bool updateCart =await _shoppingCartRepository.Update(shoppingCart.Result);
                if (updateCart)
                {
                    await _shoppingCartRepository.SaveChange();
                    response.Data = shoppingCart.Result;

                    shoppingCart.Result.AddDomainEvent(new AddProductToShoppingCartEvent(shoppingCart.Result));

                    return response;
                }
            }

            response.IsSuccess = false;
            response.Message = "Failed";

            return response;
        }
    }
}
