
namespace Cart.Data.Entities
{
    public class ProductInventory : BaseEntity
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
