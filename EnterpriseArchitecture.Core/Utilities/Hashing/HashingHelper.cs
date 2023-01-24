using System.Text;

namespace EnterpriseArchitecture.Core.Utilities.Hashing;

public static class HashingHelper
{
    public static void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return !computeHash.Where((t, i) => t != passwordHash[i]).Any();
    }
}