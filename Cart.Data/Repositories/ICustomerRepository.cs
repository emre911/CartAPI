using Cart.Data.Entities;

namespace Cart.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> Get(long customerNumber);
    }
}
