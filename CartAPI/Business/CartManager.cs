using Cart.Data;
using Cart.Data.Entities;
using CartAPI.Mappers;
using CartAPI.Models;
using System.Security.Claims;

namespace CartAPI.Business
{
    /// <summary>
    /// Business manager for cart processes
    /// </summary>
    public class CartManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartDataContext _cartDataContext;

        /// <summary>
        /// Cart manager constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="cartDataContext"></param>
        public CartManager(IHttpContextAccessor httpContextAccessor, ICartDataContext cartDataContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartDataContext = cartDataContext;
        }

        /// <summary>
        /// Get cart info with user id
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public async Task<CartOutputModel> GetCart()
        {
            User user = await GetUser();
            ShoppingCart cart = await _cartDataContext.CartRepository.Get(user.Id);

            if (cart == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "No shopping cart found for this userId." } };

            CartOutputModel output = new();
            output.CartId = cart.Id;

            List<CartItem> cartItems = await _cartDataContext.CartItemRepository.Get(cart.Id);

            if (cartItems == null)
                return output;

            return CartMapper.MapCartOutputModel(cart, cartItems);
        }

        /// <summary>
        /// Add new item to cart
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<CartOutputModel> AddCart(int productId)
        {
            Product product = await _cartDataContext.ProductRepository.Get(productId);

            if (product == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "No product found with this productId." } };

            User user = await GetUser();

            if (user == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Active user is not found for this session." } };

            ShoppingCart cart = await _cartDataContext.CartRepository.Get(user.Id);

            if (cart == null)
            {
                cart = new ShoppingCart() { CreatedOn = DateTime.Now, CreatedBy = "ciceksepeti", IsActive = true, UserId = user.Id };
                cart.Id = await _cartDataContext.CartRepository.Add(cart);
            }

            ProductInventory inventory = await _cartDataContext.ProductInventoryRepository.Get(productId);

            if (inventory == null || inventory.Quantity <= 0)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Insufficient stock for this product." } };

            List<CartItem> cartItems = await _cartDataContext.CartItemRepository.Get(cart.Id);
            CartItem? productInCart = cartItems?.Where(c => c.ProductId == productId)?.FirstOrDefault();

            if (cartItems == null || productInCart == null)
            {
                productInCart = new CartItem() { CreatedOn = DateTime.Now, CreatedBy = "ciceksepeti", IsActive = true, CartId = cart.Id, ProductId = productId, Quantity = 1 };
                productInCart.Id = await _cartDataContext.CartItemRepository.Add(productInCart);
                return new CartOutputModel() { IsSucceeded = true };
            }

            productInCart.Quantity++;

            if(productInCart.Quantity > inventory.Quantity)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Insufficient stock for this product." } };

            await _cartDataContext.CartItemRepository.Update(productInCart);

            return CartMapper.MapCartOutputModel(cart, cartItems);
        }

        /// <summary>
        /// Remove from cart
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<CartOutputModel> RemoveCart(int productId)
        {
            Product product = await _cartDataContext.ProductRepository.Get(productId);

            if (product == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "No product found with this productId." } };

            User user = await GetUser();

            if (user == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Active user is not found for this session." } };

            ShoppingCart cart = await _cartDataContext.CartRepository.Get(user.Id);

            if (cart == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Shopping cart is not found for this user." } };

            CartOutputModel output = new()
            {
                CartId = cart.Id
            };

            List<CartItem> cartItems = await _cartDataContext.CartItemRepository.Get(cart.Id);

            if (cartItems == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Shopping cart is empty." } };

            CartItem? productInCart = cartItems.Where(c => c.ProductId == productId)?.FirstOrDefault();

            if (productInCart == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "This product is not already in the cart." } };

            productInCart.Quantity--;

            if (productInCart.Quantity > 0)
                await _cartDataContext.CartItemRepository.Update(productInCart);
            else
                await _cartDataContext.CartItemRepository.Delete(productInCart);

            cartItems = cartItems.Where(x => x.Quantity > 0).ToList();

            return CartMapper.MapCartOutputModel(cart, cartItems);
        }      

        /// <summary>
        /// Update cart item in cart
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<CartOutputModel> UpdateCart(CartItemInputModel inputModel)
        {
            Product product = await _cartDataContext.ProductRepository.Get(inputModel.ProductId);

            if (product == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "No product found with this productId." } };

            User user = await GetUser();

            if (user == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Active user is not found for this session." } };

            ShoppingCart cart = await _cartDataContext.CartRepository.Get(user.Id);

            if (cart == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Shopping cart is not found for this user." } };

            CartOutputModel output = new()
            {
                CartId = cart.Id
            };

            List<CartItem> cartItems = await _cartDataContext.CartItemRepository.Get(cart.Id);

            if (cartItems == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Shopping cart is empty." } };

            CartItem? productInCart = cartItems.Where(c => c.ProductId == inputModel.ProductId)?.FirstOrDefault();

            if (productInCart == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "This product is not already in the cart." } };

            if(inputModel.Quantity < 0)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Quantity can not be smaller than 0." } };

            productInCart.Quantity = inputModel.Quantity;

            ProductInventory inventory = await _cartDataContext.ProductInventoryRepository.Get(inputModel.ProductId);

            if (inventory == null || inventory.Quantity <= 0 || productInCart.Quantity > inventory.Quantity)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Insufficient stock for this product." } };

            if (productInCart.Quantity > 0)
                await _cartDataContext.CartItemRepository.Update(productInCart);
            else
                await _cartDataContext.CartItemRepository.Delete(productInCart);

            cartItems = cartItems.Where(x => x.Quantity > 0).ToList();

            return CartMapper.MapCartOutputModel(cart, cartItems);
        }

        /// <summary>
        /// Delete cart item from cart
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<CartOutputModel> DeleteCart()
        {
            User user = await GetUser();

            if (user == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Active user is not found for this session." } };

            ShoppingCart cart = await _cartDataContext.CartRepository.Get(user.Id);

            if (cart == null)
                return new CartOutputModel() { IsSucceeded = false, Messages = new List<string> { "Shopping cart is not found for this user." } };

            await _cartDataContext.CartItemRepository.DeleteAll(cart.Id);

            return new CartOutputModel() { IsSucceeded = true};
        }

        public async Task<User> GetUser()
        {
            string userName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? String.Empty;
            return await _cartDataContext.UserRepository.Get(userName);
        }
    }
}
