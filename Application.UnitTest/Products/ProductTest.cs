using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using ShoppingCart_Application.Services.Commands.Products;
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

        [Fact]
        public async Task Handle_Should_Return_True_When_Input_Is_Valid()
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
            var validateResult =_validator.TestValidate(command).Result;
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.True(validateResult.IsValid);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            _mockRepository.Verify(x => x.Add(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_False_When_Input_Is_Invalid()
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

    }
}
