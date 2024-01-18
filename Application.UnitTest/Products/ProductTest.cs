using Moq;
using ShoppingCart_Application.Services.Commands.Products;
using ShoppingCart_Domain.Entities;
using ShoppingCart_Domain.ValueObjects;
using ShoppingCart_infrastructure.Repositories;

namespace Application.UnitTest.Products
{
    public class ProductTest
    {
        [Fact]
        public async Task Handle_Should_Return_True_When_Input_Is_Valid()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Add(It.IsAny<Product>())).ReturnsAsync(true);

            var handler = new CreateProductCommandHandler(mockRepository.Object);

            Price price = new Price(26, "dollar");

            var command = new CreateProductCommand
            {
                Name = "name",
                Price = price,
            };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            mockRepository.Verify(x => x.Add(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Return_False_When_Input_Is_Invalid()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Add(It.IsAny<Product>())).ReturnsAsync(true);

            var handler = new CreateProductCommandHandler(mockRepository.Object);

            Price price = new Price(26, "dollar");

            var command = new CreateProductCommand
            {
                Name = string.Empty,
                Price = price,
            };

            // Act
            //var validators = new List<IValidator<CreateProductCommand>> { new CreateProductCommandValidator() };
            var result = await handler.Handle(command, default);
            

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            mockRepository.Verify(x => x.Add(It.IsAny<Product>()), Times.Never);
        }

    }
}
