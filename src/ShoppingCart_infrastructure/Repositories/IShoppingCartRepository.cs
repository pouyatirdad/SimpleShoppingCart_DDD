using ShoppingCart_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
