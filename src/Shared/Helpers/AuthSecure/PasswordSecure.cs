using System.Security.Cryptography;
using System.Text;

namespace PlcBase.Shared.Helpers;

public static class PasswordSecure
{
    public static PasswordHash GetPasswordHash(string password)
    {
        using HMACSHA512 hmac = new HMACSHA512();
        byte[] passwordByte = Encoding.UTF8.GetBytes(password);
        return new PasswordHash() { PasswordHashed = hmac.ComputeHash(passwordByte), PasswordSalt = hmac.Key };
    }

    public static bool IsValidPasswod(string password, PasswordHash passwordHash)
    {
        using HMACSHA512 hmac = new HMACSHA512(passwordHash.PasswordSalt);
        byte[] passwordByte = Encoding.UTF8.GetBytes(password);
        byte[] computedHash = hmac.ComputeHash(passwordByte);
        return computedHash.SequenceEqual(passwordHash.PasswordHashed);
    }
}
