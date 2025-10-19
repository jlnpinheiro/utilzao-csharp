using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JNogueira.Utilzao
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Criptografa uma string (AES-256/CBC).
        /// </summary>
        public static string Criptografar(this string texto, Guid chave)
        {
            string guid = chave.ToString();
            const int keysize = 256;
            const int saltSize = 16;
            const int ivSize = 16;
            const int iterations = 150_000;

            var salt = new byte[saltSize];
            var iv = new byte[ivSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                rng.GetBytes(iv);
            }

            using var kdf = new Rfc2898DeriveBytes(guid, salt, iterations, HashAlgorithmName.SHA256);
            var keyBytes = kdf.GetBytes(keysize / 8);

            var plainTextBytes = Encoding.UTF8.GetBytes(texto);

            using var aes = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7, KeySize = keysize, BlockSize = 128 };
            using var encryptor = aes.CreateEncryptor(keyBytes, iv);
            using var memoryStream = new MemoryStream();
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
            }

            var cipherTextBytes = memoryStream.ToArray();

            var output = new byte[salt.Length + iv.Length + cipherTextBytes.Length];
            Buffer.BlockCopy(salt, 0, output, 0, salt.Length);
            Buffer.BlockCopy(iv, 0, output, salt.Length, iv.Length);
            Buffer.BlockCopy(cipherTextBytes, 0, output, salt.Length + iv.Length, cipherTextBytes.Length);

            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// Descriptografa uma string (AES-256/CBC).
        /// </summary>
        public static string Descriptografar(this string texto, Guid chave)
        {
            string guid = chave.ToString();
            const int keysize = 256;
            const int saltSize = 16;
            const int ivSize = 16;
            const int iterations = 150_000;

            var allBytes = Convert.FromBase64String(texto);
            if (allBytes.Length < (saltSize + ivSize + 1))
                throw new CryptographicException("Dados inválidos.");

            var salt = new byte[saltSize];
            var iv = new byte[ivSize];
            var cipherTextBytes = new byte[allBytes.Length - saltSize - ivSize];

            Buffer.BlockCopy(allBytes, 0, salt, 0, saltSize);
            Buffer.BlockCopy(allBytes, saltSize, iv, 0, ivSize);
            Buffer.BlockCopy(allBytes, saltSize + ivSize, cipherTextBytes, 0, cipherTextBytes.Length);

            using var kdf = new Rfc2898DeriveBytes(guid, salt, iterations, HashAlgorithmName.SHA256);
            var keyBytes = kdf.GetBytes(keysize / 8);

            using var aes = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7, KeySize = keysize, BlockSize = 128 };
            using var decryptor = aes.CreateDecryptor(keyBytes, iv);
            using var memoryStream = new MemoryStream(cipherTextBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var plainMs = new MemoryStream();
            cryptoStream.CopyTo(plainMs);

            return Encoding.UTF8.GetString(plainMs.ToArray());
        }
    }
}
