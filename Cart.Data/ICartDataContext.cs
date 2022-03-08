using Cart.Data.Entities;
using Cart.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cart.Data
{
    public interface ICartDataContext
    {
        public DatabaseFacade Database { get; }
        DbSet<User> User { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<ShoppingCart> ShoppingCart { get; set; }
        DbSet<CartItem> CartItem { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<ProductInventory> ProductInventory { get; set; }
        DbSet<Brand> Brand { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IUserRepository UserRepository { get; set; }
        ICartRepository CartRepository { get; set; }
        ICartItemRepository CartItemRepository { get; set; }
        IProductRepository ProductRepository { get; set; }
        IProductInventoryRepository ProductInventoryRepository { get; set; }
        IBrandRepository BrandRepository { get; set; }
    }
}
