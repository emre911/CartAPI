using Cart.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Data.Repositories
{
    public class ProductInventoryRepository : IProductInventoryRepository
    {
        private readonly ICartDataContext _context;
        public ProductInventoryRepository(ICartDataContext context)
        {
            _context = context;
        }
        public async Task<ProductInventory> Get(int productId)
        {
            return await _context.ProductInventory.Where(c => c.ProductId == productId)?.FirstOrDefaultAsync();
        }
    }
}
