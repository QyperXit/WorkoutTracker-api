using BCrypt.Net;

public class PasswordHelper
{
    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return null;

        // Generate a salted hash of the password
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Verify the password against the stored hash
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}