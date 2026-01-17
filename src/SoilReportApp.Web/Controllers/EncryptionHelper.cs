using System.Security.Cryptography;
using System.Text;

class EncryptionHelper
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef"); // 32-byte key

    public static string Encrypt(string plainText)
    {
        using (AesGcm aes = new AesGcm(Key))
        {
            byte[] nonce = new byte[AesGcm.NonceByteSizes.MaxSize]; // 12-byte IV (nonce)
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherBytes = new byte[plainBytes.Length];
            byte[] tag = new byte[AesGcm.TagByteSizes.MaxSize]; // 16-byte authentication tag

            RandomNumberGenerator.Fill(nonce); // Generate a secure random nonce

            aes.Encrypt(nonce, plainBytes, cipherBytes, tag);

            byte[] result = new byte[nonce.Length + cipherBytes.Length + tag.Length];
            Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
            Buffer.BlockCopy(cipherBytes, 0, result, nonce.Length, cipherBytes.Length);
            Buffer.BlockCopy(tag, 0, result, nonce.Length + cipherBytes.Length, tag.Length);

            return Convert.ToBase64String(result);
        }
    }

    public static string Decrypt(string encryptedText)
    {
        byte[] encryptedData = Convert.FromBase64String(encryptedText);
        byte[] nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        byte[] tag = new byte[AesGcm.TagByteSizes.MaxSize];
        byte[] cipherBytes = new byte[encryptedData.Length - nonce.Length - tag.Length];

        Buffer.BlockCopy(encryptedData, 0, nonce, 0, nonce.Length);
        Buffer.BlockCopy(encryptedData, nonce.Length, cipherBytes, 0, cipherBytes.Length);
        Buffer.BlockCopy(encryptedData, nonce.Length + cipherBytes.Length, tag, 0, tag.Length);

        using (AesGcm aes = new AesGcm(Key))
        {
            byte[] plainBytes = new byte[cipherBytes.Length];
            aes.Decrypt(nonce, cipherBytes, tag, plainBytes);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}