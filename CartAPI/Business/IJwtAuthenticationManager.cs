using Cart.Data.Entities;
using CartAPI.Models;

namespace CartAPI.Business
{
    public interface IJwtAuthenticationManager
    {
        Token CreateToken(User appUser);
    }
}
