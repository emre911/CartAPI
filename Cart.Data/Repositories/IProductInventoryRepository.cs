using Cart.Data.Entities;

namespace Cart.Data.Repositories
{
    public interface IProductInventoryRepository
    {
        Task<ProductInventory> Get(int productId);
    }
}
