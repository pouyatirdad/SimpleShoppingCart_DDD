using MediatR;
using ShoppingCart_Application.Responses;
using ShoppingCart_Domain.Entities;
using ShoppingCart_Domain.ValueObjects;
using ShoppingCart_infrastructure.Repositories;

namespace ShoppingCart_Application.Services.Commands.Products
{
    public class AddProductCommand : IRequest<Response<Product>>
    {
        public string Name { get; private set; }
        public Price Price { get; private set; }
    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Response<Product>>
    {
        private readonly IProductRepository _productRepository;
        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<Product>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<Product>();

            Guid Id = Guid.NewGuid();
            var newProduct = new Product(Id, request.Name, request.Price);

            bool result = await _productRepository.Add(newProduct);

            if (result)
            {
                response.Data = newProduct;
                return response;
            }

            response.IsSuccess = false;
            response.Message = "Failed";

            return response;
        }
    }

}
