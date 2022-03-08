

namespace Cart.Data.Entities
{
    public class Customer : BaseEntity
    {
        public long CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public int YearBirth { get; set; }
        public string Gender { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
