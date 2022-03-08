using CartAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Cart.Data.Entities;

namespace CartAPI.Business
{
    public class JwtAuthenticationManger : IJwtAuthenticationManager
    {
        private readonly string key;

        public JwtAuthenticationManger(string key)
        {
            this.key = key;
        }
        public Token CreateToken(User appUser)
        {
            Token token = new Token();
            if (appUser == null)
                return null;
            token.Expiration = DateTime.Now.AddHours(2);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, appUser.Username),
                    new Claim(ClaimTypes.NameIdentifier, appUser.Username)
                }),
                Expires = token.Expiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            token.AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
