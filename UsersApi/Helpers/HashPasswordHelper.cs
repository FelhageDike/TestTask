using System.Security.Cryptography;
using System.Text;

namespace UsersApi
{
    public class HashPasswordHelper
    {
        public static string GetHashedPassword(string password)
        {
            using var sha = SHA512.Create();
            var sb = new StringBuilder();
            foreach (var item in sha.ComputeHash(Encoding.Unicode.GetBytes(password)))
            {
                sb.Append(item.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
