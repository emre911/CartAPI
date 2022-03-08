using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CartAPI.Helpers
{
    public class StringHelper
    {
        public static bool IsValidPhone(string phone)
        {
            if (phone == null)
                return false;

            if (phone.Length > 15)
                return false;

            return Regex.Match(phone, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").Success;
        }
        public static bool IsValidEmail(string email)
        {
            if (email == null)
                return false;

            if (email.Length > 255)
                return false;

            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static String Sha256Hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
