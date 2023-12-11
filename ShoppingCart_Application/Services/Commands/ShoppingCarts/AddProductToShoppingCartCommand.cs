using MediatR;
using ShoppingCart_Domain.Entities;
using ShoppingCart_infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart_Application.Services.Commands.ShoppingCarts
{
    public class AddProductToShoppingCartCommand:IRequest<ShoppingCart>
    {
        public Product Product { get; set; }
        public Guid ShoppingCartId { get; set; }
    }
    public class AddProductToShoppingCartCommandHandler : IRequestHandler<AddProductToShoppingCartCommand, ShoppingCart>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;

        public AddProductToShoppingCartCommandHandler(IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCart> Handle(AddProductToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var product = _productRepository.Get(request.Product.Id);
            var shoppingCart = _shoppingCartRepository.Get(request.ShoppingCartId);

            if (product.Result != null && shoppingCart.Result != null)
            {
                shoppingCart.Result.AddItem(request.Product);

                bool updateCart =await _shoppingCartRepository.Update(shoppingCart.Result);
            }

            return shoppingCart.Result;
        }
    }
}
