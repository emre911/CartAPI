using Cart.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Data.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ICartDataContext _context;
        public CartItemRepository(ICartDataContext context)
        {
            _context = context;
        }
        public async Task<List<CartItem>> Get(int cartId)
        {
            return await _context.CartItem.Include(cartItem => cartItem.Product).ThenInclude(cartItem => cartItem.Brand).Where(c => c.CartId == cartId && c.IsActive).ToListAsync();
        }

        public async Task<int> Add(CartItem cartItem)
        {
            _context.CartItem.Add(cartItem);
            await _context.SaveChangesAsync();

            return cartItem.Id;
        }

        public async Task Update(CartItem cartItem)
        {
            _context.CartItem.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(CartItem cartItem)
        {
            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAll(int cartId)
        {
            _context.CartItem.RemoveRange(_context.CartItem.Where(x => x.CartId == cartId));
            await _context.SaveChangesAsync();
        }
    }
}
