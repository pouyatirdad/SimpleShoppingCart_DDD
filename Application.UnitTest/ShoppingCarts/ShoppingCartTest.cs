using Moq;
using ShoppingCart_Application.Services.Commands.Products;
using ShoppingCart_Application.Services.Commands.ShoppingCarts;
using ShoppingCart_Application.Services.Queries.ShoppingCarts;
using ShoppingCart_Domain.Entities;
using ShoppingCart_Domain.ValueObjects;
using ShoppingCart_infrastructure.Repositories;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UnitTest.ShoppingCarts
{
    public class ShoppingCartTest
    {
        #region CreateShoppingCart
        [Fact]
        public async Task CreateShoppingCart_Should_Return_True_When_Input_Is_Valid()
        {
            // Arrange
            var shoppingCartMockRepository = new Mock<IShoppingCartRepository>();
            shoppingCartMockRepository.Setup(x => x.Add(It.IsAny<ShoppingCart>())).ReturnsAsync(true);

            var command = new CreateShoppingCartCommand();
            var handler = new CreateShoppingCartCommandHandler(shoppingCartMockRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.True(Guid.TryParse(result.Data.ToString(), out _));
            shoppingCartMockRepository.Verify(x => x.Add(It.IsAny<ShoppingCart>()), Times.Once);
        }
        #endregion
        #region AddProductToShoppingCart
        [Fact]
        public async Task AddProductToShoppingCart_Should_Return_True_When_Input_Is_Valid()
        {
            // Arrange
            var shoppingCartMockRepository = new Mock<IShoppingCartRepository>();
            var productMockRepository = new Mock<IProductRepository>();

            var firstPrice = new Price(1000, "dollar");
            var secondPrice = new Price(1200, "dollar");

            var firstProduct = new Product(Guid.NewGuid(), "first", firstPrice);
            var secondProduct = new Product(Guid.NewGuid(), "second", secondPrice);

            var command = new AddProductToShoppingCartCommand
            {
                ProductsId = new List<Guid>() { firstProduct.Id, secondProduct.Id },
                ShoppingCartId = Guid.NewGuid()
            };

            var shoppingCart = new ShoppingCart(command.ShoppingCartId);
            shoppingCartMockRepository.Setup(x => x.Get(command.ShoppingCartId)).ReturnsAsync(shoppingCart);
            shoppingCartMockRepository.Setup(x => x.Update(It.IsAny<ShoppingCart>())).ReturnsAsync(true);

            productMockRepository.Setup(x => x.Get(firstProduct.Id)).ReturnsAsync(firstProduct);
            productMockRepository.Setup(x => x.Get(secondProduct.Id)).ReturnsAsync(secondProduct);

            var handler = new AddProductToShoppingCartCommandHandler(productMockRepository.Object, shoppingCartMockRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);

            Assert.Equal(2, result.Data.Items.Count);
            var actualTotal = result.Data.Total.Amount;
            var expectedTotal = result.Data.Items[0].Price.Amount + result.Data.Items[1].Price.Amount;
            Assert.Equal(expectedTotal, actualTotal);
            Assert.Equal(result.Data.Id, command.ShoppingCartId);
        }
        #endregion
        #region GetShoppingCarts
        [Fact]
        public async Task GetShoppingCarts_Should_Return_True()
        {
            // Arrange
            var shoppingCartMockRepository = new Mock<IShoppingCartRepository>();

            var firstPrice = new Price(1000, "dollar");
            var secondPrice = new Price(1200, "dollar");
            var firstProduct = new Product(Guid.NewGuid(), "first", firstPrice);
            var secondProduct = new Product(Guid.NewGuid(), "second", secondPrice);

            var firstShoppingCart = new ShoppingCart(Guid.NewGuid());
            firstShoppingCart.AddItem(firstProduct);
            firstShoppingCart.AddItem(secondProduct);

            var shoppingCarts = new List<ShoppingCart>()
            {
                firstShoppingCart
            };

            shoppingCartMockRepository.Setup(x => x.GetAll()).ReturnsAsync(shoppingCarts);

            var query = new GetShoppingCartsQuery();
            var handler = new GetShoppingCartsQueryHandler(shoppingCartMockRepository.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);

            Assert.Equal(2, result.Data[0].Items.Count);
            var actualTotal = result.Data[0].Total.Amount;
            var expectedTotal = result.Data[0].Items[0].Price.Amount + result.Data[0].Items[1].Price.Amount;
            Assert.Equal(expectedTotal, actualTotal);
            Assert.Equal(result.Data[0].Id, firstShoppingCart.Id);

            shoppingCartMockRepository.Verify(x => x.GetAll(), Times.Once);
        }
        #endregion
        #region GetShoppingCartById
        [Fact]
        public async Task GetShoppingCartById_Should_Return_True()
        {
            // Arrange
            var shoppingCartMockRepository = new Mock<IShoppingCartRepository>();

            var firstPrice = new Price(1000, "dollar");
            var secondPrice = new Price(1200, "dollar");
            var firstProduct = new Product(Guid.NewGuid(), "first", firstPrice);
            var secondProduct = new Product(Guid.NewGuid(), "second", secondPrice);

            var id = Guid.NewGuid();

            var shoppingCart = new ShoppingCart(id);
            shoppingCart.AddItem(firstProduct);
            shoppingCart.AddItem(secondProduct);

            shoppingCartMockRepository.Setup(x => x.Get(id)).ReturnsAsync(shoppingCart);

            var query = new GetShoppingCartByIdQuery()
            {
                ShoppingCartId=id
            };
            var handler = new GetShoppingCartByIdQueryHandler(shoppingCartMockRepository.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);

            Assert.Equal(2, result.Data.Items.Count);
            var actualTotal = result.Data.Total.Amount;
            var expectedTotal = result.Data.Items[0].Price.Amount + result.Data.Items[1].Price.Amount;
            Assert.Equal(expectedTotal, actualTotal);
            Assert.Equal(result.Data.Id, shoppingCart.Id);

            shoppingCartMockRepository.Verify(x => x.Get(id), Times.Once);
        }
        #endregion
    }
}
