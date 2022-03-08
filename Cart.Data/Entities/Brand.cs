
namespace Cart.Data.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
