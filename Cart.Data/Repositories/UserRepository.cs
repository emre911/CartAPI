using Cart.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICartDataContext _context;
        public UserRepository(ICartDataContext context)
        {
            _context = context;
        }

        public async Task<User> Get(string userName)
        {
            return await _context.User.Where(c => c.Username == userName).FirstOrDefaultAsync();
        }
    }
}
