using BC = BCrypt.Net.BCrypt;

namespace PlcBase.Shared.Utilities;

public static class PasswordUtility
{
    public static string GetPasswordHash(string password)
    {
        return BC.HashPassword(password);
    }

    public static bool IsValidPassword(string password, string passwordHash)
    {
        return BC.Verify(password, passwordHash);
    }
}
