using System.Security.Cryptography;
using System.Text;

namespace _3ai.solutions.Hasher
{
    public sealed class AesCipher
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public AesCipher(string key, string iv)
        {
            _key = Encoding.UTF8.GetBytes(key);
            _iv = Encoding.UTF8.GetBytes(iv);
        }

        public string Encrypt(string plainText, string? altIv = null)
        {
            using Aes aes = Aes.Create();
            aes.Key = _key;
            aes.IV = altIv is null ? _iv : Encoding.UTF8.GetBytes(altIv);
            using MemoryStream msEncrypt = new();
            using ICryptoTransform encoder = aes.CreateEncryptor();
            using (CryptoStream csEncrypt = new(msEncrypt, encoder, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new(csEncrypt))
                swEncrypt.Write(plainText);
            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string cipherText, string? altIv = null)
        {
            using Aes aes = Aes.Create();
            aes.Key = _key;
            aes.IV = altIv is null ? _iv : Encoding.UTF8.GetBytes(altIv);
            using MemoryStream msDecryptor = new(Convert.FromBase64String(cipherText));
            using ICryptoTransform decoder = aes.CreateDecryptor();
            using CryptoStream csDecryptor = new(msDecryptor, decoder, CryptoStreamMode.Read);
            using StreamReader srReader = new(csDecryptor);
            return srReader.ReadToEnd();
        }
    }
}