using MediatR;
using ShoppingCart_Application.Responses;
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
    public class CreateShoppingCartCommand:IRequest<Response<Guid>>
    {
    }
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, Response<Guid>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public CreateShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Response<Guid>> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var response=new Response<Guid>();

            Guid Id =Guid.NewGuid();
            var newShoppingCart = new ShoppingCart(Id);

            bool result=await _shoppingCartRepository.Add(newShoppingCart);

            if (result)
            {
                response.Data = Id;
                return response;
            }

            response.IsSuccess = false;
            response.Message = "Failed";

            return response;
        }
    }
}
