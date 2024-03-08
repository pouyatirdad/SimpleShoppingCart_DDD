using Microsoft.EntityFrameworkCore;
using ShoppingCart_Domain.Entities;
using ShoppingCart_infrastructure.Context;

namespace ShoppingCart_infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingCartContext _context;

        public ProductRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            Product cart = await Get(Id);
            if (cart != null)
            {
                return await Delete(cart);
            }
            return false;
        }

        public async Task<bool> Delete(Product product)
        {
            try
            {
                _context.Products.Remove(product);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Product> Get(Guid Id)
        {
            return await _context.Products.FindAsync(Id);
        }
        public async Task<bool> Update(Product product)
        {
            try
            {
                _context.Products.Update(product);
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

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }
    }
}
