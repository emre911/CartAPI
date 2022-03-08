using Cart.Data.Entities;

namespace Cart.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Get(int productId);
    }
}
