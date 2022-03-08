using CartAPI.Helpers;

namespace CartAPI.Validations
{
    public class UserValidator
    {
        public static bool ValidatePassword(string userPassword, string passwordEntered)
        {
            string sha256Password = StringHelper.Sha256Hash(passwordEntered);

            return sha256Password == userPassword;
        }
    }
}
