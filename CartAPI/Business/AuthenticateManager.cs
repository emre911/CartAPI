using Cart.Data.Repositories;
using CartAPI.Models;
using CartAPI.Validations;

namespace CartAPI.Business
{
    public class AuthenticateManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public AuthenticateManager(IUserRepository userRepository, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _userRepository = userRepository;
        }

        public async Task<Token?> AuthenticateAsync(string userName, string password)
        {
            var user = await _userRepository.Get(userName);

            if (user == null)
                return null;

            bool validated = UserValidator.ValidatePassword(user.Password, password);

            if (!validated)
                return null;

            return _jwtAuthenticationManager.CreateToken(user);
        }


    }
}
