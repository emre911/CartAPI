using Cart.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICartDataContext _context;
        public ProductRepository(ICartDataContext context)
        {
            _context = context;
        }
        public async Task<Product> Get(int productId)
        {
            return await _context.Product.Where(c => c.Id == productId).FirstOrDefaultAsync();
        }
    }
}
