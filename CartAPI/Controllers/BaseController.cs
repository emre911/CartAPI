using Cart.Data;
using Cart.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartAPI.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;
        protected readonly IConfiguration _configuration;
        protected readonly ICartDataContext _cartDataContext;

        public BaseController(ILogger<BaseController> logger, IConfiguration configuration, ICartDataContext cartDataContext)
        {
            _logger = logger;
            _configuration = configuration;
            _cartDataContext = cartDataContext;
        }
    }
}
