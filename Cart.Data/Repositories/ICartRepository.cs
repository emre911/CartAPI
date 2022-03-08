
using Cart.Data.Entities;

namespace Cart.Data.Repositories
{
    public interface ICartRepository
    {
        Task<ShoppingCart> Get(int userId);
        Task<int> Add(ShoppingCart cart);
    }
}
