using Cart.Data;
using Cart.Data.Repositories;
using CartAPI.Business;
using CartAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartAPI.Controllers
{
    /// <summary>
    /// Used for transactions related to customer carts
    /// </summary>
    [ApiController]
    [Route("api/Cart/[action]")]
    public class CartController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor for CartController
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="accountRepository"></param>
        /// <param name="customerRepository"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="cartDataContext"></param>
        public CartController(IHttpContextAccessor httpContextAccessor, ILogger<CartController> logger, IConfiguration configuration, ICartDataContext cartDataContext,
            IUserRepository userRepository, IProductRepository productRepository, IProductInventoryRepository productInventoryRepository,
           IBrandRepository brandRepository, ICartRepository cartRepository, ICartItemRepository cartItemRepository) : base(logger, configuration, cartDataContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartDataContext.UserRepository = userRepository;
            _cartDataContext.BrandRepository = brandRepository;
            _cartDataContext.ProductRepository = productRepository;
            _cartDataContext.ProductInventoryRepository = productInventoryRepository;
            _cartDataContext.CartRepository = cartRepository;
            _cartDataContext.CartItemRepository = cartItemRepository;
        }

        /// <summary>
        /// Get all products in shopping cart
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Get")]
        public async Task<IActionResult> Get()
        {
            CartOutputModel response = await new CartManager(_httpContextAccessor, _cartDataContext).GetCart();

            return Ok(response);
        }

        /// <summary>
        /// Add new product to shopping cart
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(int productId)
        {
            CartOutputModel response = await new CartManager(_httpContextAccessor, _cartDataContext).AddCart(productId);

            return Ok(response);
        }

        /// <summary>
        /// Remove the product from shopping cart
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Remove")]
        public async Task<IActionResult> Remove(int productId)
        {
            CartOutputModel response = await new CartManager(_httpContextAccessor, _cartDataContext).RemoveCart(productId);

            return Ok(response);
        }

        /// <summary>
        /// Update shopping cart item
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update(CartItemInputModel inputModel)
        {
            CartOutputModel response = await new CartManager(_httpContextAccessor, _cartDataContext).UpdateCart(inputModel);

            return Ok(response);
        }

        /// <summary>
        /// Delete selected cart items from the shopping cart
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete()
        {
            CartOutputModel response = await new CartManager(_httpContextAccessor, _cartDataContext).DeleteCart();

            return Ok(response);
        }
    }
}