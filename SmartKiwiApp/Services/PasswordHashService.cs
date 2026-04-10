using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace SmartKiwiApp.Services;
public class PasswordHashService
{
    private const int HashSize = 32;
    private const int SaltSize = 16;
    private const int Iterations = 3;
    private const int MemorySize = 65536;
    private const int DegreeOfParallelism = 8;
    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            MemorySize = MemorySize,
            Iterations = Iterations
        };

        var hash = argon2.GetBytes(HashSize);

        var result = new byte[SaltSize + HashSize];
        Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
        Buffer.BlockCopy(hash, 0, result, SaltSize, HashSize);

        return Convert.ToBase64String(result);
    }

    public bool VerifyPassword(string informedPassword, string hashedPassword)
    {
        var fullHash = Convert.FromBase64String(hashedPassword);

        var salt = new byte[SaltSize];
        Buffer.BlockCopy(fullHash, 0, salt, 0, SaltSize);

        var expectedHash = new byte[HashSize];
        Buffer.BlockCopy(fullHash, SaltSize, expectedHash, 0, HashSize);

        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(informedPassword))
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            MemorySize = MemorySize,
            Iterations = Iterations
        };

        var actualHash = argon2.GetBytes(HashSize);

        return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
    }
}