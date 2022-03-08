using Cart.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ICartDataContext _context;
        public BrandRepository(ICartDataContext context)
        {
            _context = context;
        }
        public async Task<Brand> Get(int brandId)
        {
            return await _context.Brand.Where(c => c.Id == brandId && c.IsActive).FirstOrDefaultAsync();
        }
    }
}
