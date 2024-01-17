using ShoppingCart_Domain.Entities;

namespace ShoppingCart_infrastructure.Repositories
{
    public interface IShoppingCartRepository
    {
        public Task<bool> Add(ShoppingCart shoppingCart);
        public Task<bool> Update(ShoppingCart shoppingCart);
        public Task<bool> Delete(Guid Id);
        public Task<bool> Delete(ShoppingCart shoppingCart);
        public Task<ShoppingCart> Get(Guid Id);
        public Task<List<ShoppingCart>> GetAll();
        public Task SaveChange();
    }
}
