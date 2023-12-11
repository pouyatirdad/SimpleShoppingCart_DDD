using ShoppingCart_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
