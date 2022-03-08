namespace Cart.Data.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = "System";
        public string? LastUpdatedBy { get; set; }
    }
}
