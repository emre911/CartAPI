using Cart.Data.Entities;
using CartAPI.Models;

namespace CartAPI.Mappers
{
    public class CartMapper
    {
        public static CartOutputModel MapCartOutputModel(ShoppingCart cart, List<CartItem> cartItems)
        {
            CartOutputModel outputModel = new()
            {
                IsSucceeded = true,
                CartId = cart.Id,
                CartItems = MapCartItemsOutputModel(cartItems)
            };

            outputModel.CartItems?.ForEach(item => outputModel.CartAmount += (item.ProductInfo?.Price ?? 0)*item.Quantity);
            return outputModel;
        }

        public static List<CartItemOutput> MapCartItemsOutputModel(List<CartItem> cartItems)
        {
            List<CartItemOutput> cartItemsOutput = new List<CartItemOutput>();

            foreach (CartItem cartItem in cartItems)
            {
                cartItemsOutput.Add(MapCartItemOutput(cartItem));
            }

            return cartItemsOutput;
        }

        public static CartItemOutput MapCartItemOutput(CartItem cartItem)
        {
            return new CartItemOutput()
            {
                CartItemId = cartItem.Id,
                Quantity = cartItem.Quantity,
                ProductInfo = MapProductInfoOutput(cartItem.Product)
            };
        }

        public static ProductOutput MapProductInfoOutput(Product productInfo)
        {
            return new ProductOutput()
            {
                Id = productInfo.Id,
                Name = productInfo.Name,
                BrandId = productInfo.BrandId,
                BrandName = productInfo.Brand?.Name ?? String.Empty,
                Category = productInfo.Category,
                Description = productInfo.Description,
                Price = productInfo.Price,
                Sku = productInfo.Sku
            };
        }
    }
}
