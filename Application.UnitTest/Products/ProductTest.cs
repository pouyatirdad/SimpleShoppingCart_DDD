using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using ShoppingCart_Application.Services.Commands.Products;
using ShoppingCart_Application.Services.Queries.Products;
using ShoppingCart_Application.Services.Queries.ShoppingCarts;
using ShoppingCart_Domain.Entities;
using ShoppingCart_Domain.ValueObjects;
using ShoppingCart_infrastructure.Repositories;

namespace Application.UnitTest.Products
{
    public class ProductTest
    {
        private Mock<IProductRepository> _mockRepository;
        private CreateProductCommandValidator _validator;
        private CreateProductCommandHandler _handler;

        public ProductTest()
        {
            _mockRepository = new Mock<IProductRepository>();
            _validator = new CreateProductCommandValidator();
            _handler = new CreateProductCommandHandler(_mockRepository.Object);
        }
        #region CreateProduct
        [Fact]
        public async Task CreateProduct_Should_Return_True_When_Input_Is_Valid()
        {
            // Arrange
            _mockRepository.Setup(x => x.Add(It.IsAny<Product>())).ReturnsAsync(true);

            Price price = new Price(26, "dollar");

            var command = new CreateProductCommand
            {
                Name = "name",
                Price = price,
            };

            // Act
            var validateResult = _validator.TestValidate(command).Result;
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.True(validateResult.IsValid);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            _mockRepository.Verify(x => x.Add(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task CreateProduct_Should_Return_False_When_Input_Is_Invalid()
        {
            // Arrange
            _mockRepository.Setup(x => x.Add(It.IsAny<Product>())).ReturnsAsync(true);

            Price price = new Price(26, "dollar");

            var command = new CreateProductCommand
            {
                Name = string.Empty,
                Price = price,
            };

            // Act
            var validateResult = _validator.TestValidate(command).Result;
            var result = await _handler.Handle(command, default);


            // Assert
            Assert.False(validateResult.IsValid);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            _mockRepository.Verify(x => x.Add(It.IsAny<Product>()), Times.Once);
        }
        #endregion
        #region GetProductsQuery
        [Fact]
        public async Task GetProductsQuery_Should_Return_True()
        {
            // Arrange
            var productMockRepository = new Mock<IProductRepository>();

            var firstPrice = new Price(1000, "dollar");
            var secondPrice = new Price(1200, "dollar");
            var firstProduct = new Product(Guid.NewGuid(), "first", firstPrice);
            var secondProduct = new Product(Guid.NewGuid(), "second", secondPrice);

            var products=new List<Product> { firstProduct, secondProduct };

            productMockRepository.Setup(x => x.GetAll()).ReturnsAsync(products);

            var query = new GetProductsQuery();
            var handler = new GetProductsQueryHandler(productMockRepository.Object,null);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);

            productMockRepository.Verify(x => x.GetAll(), Times.Once);
        }
        #endregion
    }
}
