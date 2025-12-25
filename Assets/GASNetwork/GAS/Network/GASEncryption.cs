using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GAS.Network
{
    /// <summary>
    /// GAS API AES-256-CBC
    /// </summary>
    public static class GASEncryption
    {
        private static readonly byte[] IV = new byte[16]
        {
            0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
            0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0
        };

        /// <summary>
        /// key = SHA256(rawKey)
        /// </summary>
        public static byte[] DeriveKeyBytes(string rawKey)
        {
            SHA256 sha = SHA256.Create();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(rawKey ?? ""));
        }

        /// <summary>
        /// 加密：明文 + rawKey
        /// </summary>
        public static string Encrypt(string plainText, string rawKey)
        {
            if (string.IsNullOrEmpty(plainText)) return "";
            if (string.IsNullOrEmpty(rawKey)) return "";

            byte[] key = DeriveKeyBytes(rawKey);
            return EncryptInternal(plainText, key);
        }

        /// <summary>
        /// 解密：密文 + rawKey
        /// </summary>
        public static string Decrypt(string cipherText, string rawKey)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";
            if (string.IsNullOrEmpty(rawKey)) return "";

            byte[] key = DeriveKeyBytes(rawKey);
            return DecryptInternal(cipherText, key);
        }
        
        private static string EncryptInternal(string plainText, byte[] key)
        {
            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.IV = IV;

            using MemoryStream ms = new MemoryStream();
            using CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }
        
        private static string DecryptInternal(string cipherText, byte[] key)
        {
            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.IV = IV;

            using MemoryStream ms = new MemoryStream();
            using CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);

            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.FlushFinalBlock();

            byte[] plainBytes = ms.ToArray();
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}