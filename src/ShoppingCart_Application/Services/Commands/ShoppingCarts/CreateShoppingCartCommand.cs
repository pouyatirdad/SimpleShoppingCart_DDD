﻿using MediatR;
using ShoppingCart_Application.Responses;
using ShoppingCart_Domain.Entities;
using ShoppingCart_infrastructure.Repositories;

namespace ShoppingCart_Application.Services.Commands.ShoppingCarts
{
    #region command
    public record CreateShoppingCartCommand : IRequest<Response<Guid>>;

    #endregion
    #region handler

    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, Response<Guid>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public CreateShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Response<Guid>> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<Guid>();

            Guid Id = Guid.NewGuid();
            var newShoppingCart = new ShoppingCart(Id);

            bool result = await _shoppingCartRepository.Add(newShoppingCart);

            if (result)
            {
                await _shoppingCartRepository.SaveChange();
                response.Data = Id;
                return response;
            }

            response.IsSuccess = false;
            response.Message = "Failed";

            return response;
        }
    }
    #endregion
}
