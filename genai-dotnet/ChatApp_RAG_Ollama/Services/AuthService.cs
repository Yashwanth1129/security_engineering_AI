using ChatApp_RAG_Ollama.Models;
using System.Security.Cryptography;
using System.Text;

namespace ChatApp_RAG_Ollama.Services;

public class AuthService
{
    // In a real application, you would store users in a database
    // For demo purposes, we'll use hardcoded users
    private readonly List<User> _users = new()
    {
        new User { Username = "admin", PasswordHash = HashPassword("admin123"), Role = "Admin" },
        new User { Username = "user", PasswordHash = HashPassword("user123"), Role = "User" },
        new User { Username = "demo", PasswordHash = HashPassword("demo123"), Role = "User" }
    };

    public User? Authenticate(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username);
        if (user != null && VerifyPassword(password, user.PasswordHash))
        {
            return user;
        }
        return null;
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private static bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
}






