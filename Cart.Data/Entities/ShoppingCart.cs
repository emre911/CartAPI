
namespace Cart.Data.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
