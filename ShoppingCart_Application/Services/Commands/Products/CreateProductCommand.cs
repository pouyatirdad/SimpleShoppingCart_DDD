using FluentValidation;
using MediatR;
using ShoppingCart_Application.Responses;
using ShoppingCart_Domain.Entities;
using ShoppingCart_Domain.ValueObjects;
using ShoppingCart_infrastructure.Repositories;

namespace ShoppingCart_Application.Services.Commands.Products
{
    #region command
    public record CreateProductCommand : IRequest<Response<Product>>
    {
        public string Name { get; set; }
        public Price Price { get; set; }
    }
    #endregion
    #region handler
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<Product>>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<Product>();

            Guid Id = Guid.NewGuid();
            var newProduct = new Product(Id, request.Name, request.Price);

            bool result = await _productRepository.Add(newProduct);

            if (result)
            {
                await _productRepository.SaveChange();
                response.Data = newProduct;
                return response;
            }

            response.IsSuccess = false;
            response.Message = "Failed";

            return response;
        }
    }
    #endregion
    #region validaton

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
    #endregion
}
