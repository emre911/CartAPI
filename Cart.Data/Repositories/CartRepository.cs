using Cart.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ICartDataContext _context;
        public CartRepository(ICartDataContext context)
        {
            _context = context;
        }
        public async Task<ShoppingCart> Get(int userId)
        {
            return await _context.ShoppingCart.Where(c => c.UserId == userId && c.IsActive).FirstOrDefaultAsync();
        }

        public async Task<int> Add(ShoppingCart cart)
        {
            _context.ShoppingCart.Add(cart);
            await _context.SaveChangesAsync();

            return cart.Id;
        }
    }
}
