using System.Security.Cryptography;
using System.Text;

namespace StoneRed.NetificationApi.Utilities;

internal static class UserIdHasher
{
    public static string Hash(string userId, string clientSecret)
    {
        using HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(clientSecret));
        return Convert.ToBase64String(hmac.ComputeHash(Encoding.ASCII.GetBytes(userId)));
    }
}