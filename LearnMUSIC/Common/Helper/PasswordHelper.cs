using System.Security.Cryptography;
using System.Text;

namespace LearnMUSIC.Common.Helper
{
    public static class PasswordHelper
    {
        public static string Hash(object value)
        {
            var input = value.ToString();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider(); // MD5Hash
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            StringBuilder hash = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
