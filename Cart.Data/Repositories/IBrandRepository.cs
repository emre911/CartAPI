using Cart.Data.Entities;

namespace Cart.Data.Repositories
{
    public interface IBrandRepository
    {
        Task<Brand> Get(int brandId);
    }
}
