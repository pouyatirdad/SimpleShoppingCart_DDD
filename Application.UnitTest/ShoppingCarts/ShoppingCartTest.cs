using Moq;
using ShoppingCart_Application.Services.Commands.Products;
using ShoppingCart_Application.Services.Commands.ShoppingCarts;
using ShoppingCart_Domain.Entities;
using ShoppingCart_Domain.ValueObjects;
using ShoppingCart_infrastructure.Repositories;
using System.Diagnostics;

namespace Application.UnitTest.ShoppingCarts
{
    public class ShoppingCartTest
    {
        private Mock<IShoppingCartRepository> _mockRepository;
        private CreateShoppingCartCommandHandler _handler;

        public ShoppingCartTest()
        {
            _mockRepository = new Mock<IShoppingCartRepository>();
            _handler = new CreateShoppingCartCommandHandler(_mockRepository.Object);
        }
        #region CreateShoppingCart
        [Fact]
        public async Task CreateShoppingCart_Should_Return_True_When_Input_Is_Valid()
        {
            // Arrange
            _mockRepository.Setup(x => x.Add(It.IsAny<ShoppingCart>())).ReturnsAsync(true);

            var command = new CreateShoppingCartCommand();

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.True(Guid.TryParse(result.Data.ToString(), out _));
            _mockRepository.Verify(x => x.Add(It.IsAny<ShoppingCart>()), Times.Once);
        }
        #endregion
    }
}
