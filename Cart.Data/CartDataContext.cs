using Cart.Data.Entities;
using Cart.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cart.Data
{
    public class CartDataContext : DbContext, ICartDataContext
    {
        public CartDataContext(DbContextOptions<CartDataContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductInventory> ProductInventory { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public IUserRepository UserRepository { get; set; }
        public ICartRepository CartRepository { get; set; }
        public ICartItemRepository CartItemRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IProductInventoryRepository ProductInventoryRepository { get; set; }
        public IBrandRepository BrandRepository { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartDataContext).Assembly);
        }
    }
}
