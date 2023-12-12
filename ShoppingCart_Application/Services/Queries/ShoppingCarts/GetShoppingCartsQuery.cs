using MediatR;
using ShoppingCart_Domain.Entities;
using ShoppingCart_infrastructure.Repositories;
using ShoppingCart_Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart_Application.Services.Queries.ShoppingCarts
{
    public class GetShoppingCartsQuery : IRequest<Response<List<ShoppingCart>>>
    {
    }

    public class GetShoppingCartsQueryHandler : IRequestHandler<GetShoppingCartsQuery, Response<List<ShoppingCart>>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public GetShoppingCartsQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<Response<List<ShoppingCart>>> Handle(GetShoppingCartsQuery request, CancellationToken cancellationToken)
        {
            var response=new Response<List<ShoppingCart>>();

            var data = _shoppingCartRepository.GetAll();

            if (data.Result.Any())
            {
                response.Data = data.Result;
            }

            response.Message = "data is empty";
            response.IsSuccess = false;

            return response;
        }
    }
}
