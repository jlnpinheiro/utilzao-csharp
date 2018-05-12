using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JNogueira.Infraestrutura.Utilzao
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Criptografa uma string
        /// </summary>
        /// <param name="texto">String a ser criptografada.</param>
        public static string Criptografar(this string texto)
        {
            const string initVector = "tu89geji340t89u2";
            const string chave = "2d331cca-f6c0-40c0-bb43-6e32989c2881";
            const int keysize = 256;

            var initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            var plainTextBytes = Encoding.UTF8.GetBytes(texto);
            var password = new PasswordDeriveBytes(chave, null);
            var keyBytes = password.GetBytes(keysize / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Descriptografa uma string
        /// </summary>
        /// <param name="texto">String a ser descriptografada</param>
        public static string Descriptografar(this string texto)
        {
            const string initVector = "tu89geji340t89u2";
            const string chave = "2d331cca-f6c0-40c0-bb43-6e32989c2881";
            const int keysize = 256;

            var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            var cipherTextBytes = Convert.FromBase64String(texto);
            var password = new PasswordDeriveBytes(chave, null);
            var keyBytes = password.GetBytes(keysize / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
            var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
