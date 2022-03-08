namespace CartAPI.Models
{
    public class CartOutputModel : BaseResponse 
    {
        public int CartId { get; set; }
        public decimal CartAmount { get; set; }
        public List<CartItemOutput> CartItems { get; set; }
    }

    public class CartItemOutput
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public ProductOutput ProductInfo { get; set; }       
    }

    public class ProductOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
