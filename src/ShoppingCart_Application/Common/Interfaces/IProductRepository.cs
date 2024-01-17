using ShoppingCart_Domain.Entities;

namespace ShoppingCart_infrastructure.Repositories
{
    public interface IProductRepository
    {
        public Task<bool> Add(Product product);
        public Task<bool> Update(Product product);
        public Task<bool> Delete(Guid Id);
        public Task<bool> Delete(Product product);
        public Task<Product> Get(Guid Id);
        public Task<List<Product>> GetAll();
        public Task SaveChange();
    }
}
