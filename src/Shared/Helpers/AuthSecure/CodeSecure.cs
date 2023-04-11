using System.Security.Cryptography;

namespace PlcBase.Shared.Helpers;

public static class CodeSecure
{
    public static string CreateRandomCode(int length = 32)
    {
        byte[] randomNumber = new byte[length];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
