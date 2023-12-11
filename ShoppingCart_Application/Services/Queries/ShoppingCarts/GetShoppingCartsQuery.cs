using MediatR;
using ShoppingCart_Domain.Entities;
using ShoppingCart_infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart_Application.Services.Queries.ShoppingCarts
{
    public class GetShoppingCartsQuery : IRequest<List<ShoppingCart>>
    {
    }

    public class GetShoppingCartsQueryHandler : IRequestHandler<GetShoppingCartsQuery, List<ShoppingCart>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public GetShoppingCartsQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }
        public Task<List<ShoppingCart>> Handle(GetShoppingCartsQuery request, CancellationToken cancellationToken)
        {
            return _shoppingCartRepository.GetAll();
        }
    }
}
