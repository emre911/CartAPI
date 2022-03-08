using Cart.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICartDataContext _context;
        public CustomerRepository(ICartDataContext context)
        {
            _context = context;
        }
        public async Task<Customer> Get(long customerNumber)
        {
            return await _context.Customer.Where(c => c.CustomerNumber == customerNumber).FirstOrDefaultAsync();
        }
    }
}
