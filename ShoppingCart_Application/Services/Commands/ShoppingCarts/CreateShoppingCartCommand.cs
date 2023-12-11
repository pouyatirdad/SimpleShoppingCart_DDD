using MediatR;
using ShoppingCart_Domain.Entities;
using ShoppingCart_Domain.ValueObjects;
using ShoppingCart_infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart_Application.Services.Commands.ShoppingCarts
{
    public class CreateShoppingCartCommand:IRequest<Guid>
    {
    }
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, Guid>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public CreateShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Guid> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            Guid Id =Guid.NewGuid();
            var newShoppingCart = new ShoppingCart(Id);

            bool result=await _shoppingCartRepository.Add(newShoppingCart);

            if (result)
            {
                return Id;
            }

            return Guid.Empty;
        }
    }
}
