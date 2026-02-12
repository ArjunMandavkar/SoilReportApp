using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace SoilReportApp.Application.Security;

/// <summary>
/// Password hashing helper using PBKDF2 (Rfc2898DeriveBytes).
/// Stores version, iterations, salt and hash in a single Base64 string.
/// </summary>
public static class PasswordHasher
{
    private const int SaltSize = 16; // 128-bit salt
    private const int KeySize = 32;  // 256-bit subkey
    private const int Iterations = 100_000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    /// <summary>
    /// Hashes the provided password using PBKDF2 with a random salt.
    /// </summary>
    public static string Hash(string password)
    {
        if (password is null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            Algorithm,
            KeySize);

        // Layout: [version:1][iterations:4][salt:16][hash:32]
        byte[] result = new byte[1 + sizeof(int) + SaltSize + KeySize];
        result[0] = 1; // version

        BinaryPrimitives.WriteInt32BigEndian(result.AsSpan(1, sizeof(int)), Iterations);
        Buffer.BlockCopy(salt, 0, result, 1 + sizeof(int), SaltSize);
        Buffer.BlockCopy(hash, 0, result, 1 + sizeof(int) + SaltSize, KeySize);

        return Convert.ToBase64String(result);
    }

    /// <summary>
    /// Verifies that the provided password matches the stored hash.
    /// </summary>
    public static bool Verify(string storedHash, string password)
    {
        if (string.IsNullOrWhiteSpace(storedHash) || password is null)
        {
            return false;
        }

        byte[] decoded;
        try
        {
            decoded = Convert.FromBase64String(storedHash);
        }
        catch (FormatException)
        {
            // Not a valid Base64 string
            return false;
        }

        if (decoded.Length < 1 + sizeof(int) + SaltSize + KeySize)
        {
            return false;
        }

        byte version = decoded[0];
        if (version != 1)
        {
            // Unknown version
            return false;
        }

        int iterations = BinaryPrimitives.ReadInt32BigEndian(decoded.AsSpan(1, sizeof(int)));

        byte[] salt = new byte[SaltSize];
        Buffer.BlockCopy(decoded, 1 + sizeof(int), salt, 0, SaltSize);

        byte[] expectedHash = new byte[KeySize];
        Buffer.BlockCopy(decoded, 1 + sizeof(int) + SaltSize, expectedHash, 0, KeySize);

        byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            Algorithm,
            KeySize);

        return CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);
    }
}

