using Cart.Data.Entities;

namespace Cart.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(string userName);
    }
}
