using System.Security.Cryptography;
using System.Text;

namespace OnlyFundsAPI.Utilities
{
    public static class PasswordUtils
    {
        public static string HashString(string rawString)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //encode to byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawString));

                //convert byte array to string
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
    }
}