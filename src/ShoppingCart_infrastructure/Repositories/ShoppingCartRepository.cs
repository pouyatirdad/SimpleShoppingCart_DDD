using Microsoft.EntityFrameworkCore;
using ShoppingCart_Domain.Entities;
using ShoppingCart_infrastructure.Context;

namespace ShoppingCart_infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShoppingCartContext _context;

        public ShoppingCartRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ShoppingCart shoppingCart)
        {
            try
            {
                await _context.ShoppingCarts.AddAsync(shoppingCart);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            ShoppingCart cart = await Get(Id);
            if (cart != null)
            {
                return await Delete(cart);
            }
            return false;
        }

        public async Task<bool> Delete(ShoppingCart shoppingCart)
        {
            try
            {
                _context.ShoppingCarts.Remove(shoppingCart);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ShoppingCart> Get(Guid Id)
        {
            return await _context.ShoppingCarts.FindAsync(Id);
        }
        public async Task<bool> Update(ShoppingCart shoppingCart)
        {
            try
            {
                _context.ShoppingCarts.Update(shoppingCart);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShoppingCart>> GetAll()
        {
            return await _context.ShoppingCarts.AsNoTracking().ToListAsync();
        }
    }
}
