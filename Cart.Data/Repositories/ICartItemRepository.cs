
using Cart.Data.Entities;

namespace Cart.Data.Repositories
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> Get(int cartId);
        Task<int> Add(CartItem cartItem);
        Task Delete(CartItem cartItem);
        Task DeleteAll(int cartId);
        Task Update(CartItem cartItem);
    }
}
