using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Xamarin.Forms;
using CryptoStream = System.Security.Cryptography.CryptoStream;
using CryptoStreamMode = System.Security.Cryptography.CryptoStreamMode;

namespace ShoppingAssistant.Crypt
{
    /// <summary>
    /// Static encryption class
    /// </summary>
    public static class Crypt
    {
        /// <summary>
        /// Encryption key
        /// </summary>
        private static readonly string EncKey = DependencyService.Get<IIdentifier>().GetIdentifier();

        /// <summary>
        /// Encrypt the given string using device identifier as a key
        /// </summary>
        /// <param name="inText"></param>
        /// <returns></returns>
        public static string Encrypt(string inText)
        {
            return Encrypt(inText, EncKey);
        }

        /// <summary>
        /// Decrypt the given string using device identifier as a key
        /// </summary>
        /// <param name="inText"></param>
        /// <returns></returns>
        public static string Decrypt(string inText)
        {
            return Decrypt(inText, EncKey);
        }

        /// <summary>
        /// Encrypt the given string using the given key
        /// </summary>
        /// <param name="inText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string Encrypt(string inText, string key)
        {
            byte[] bytesBuff = Encoding.Unicode.GetBytes(inText);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new Rfc2898DeriveBytes(key,
                    new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream =
                        new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cStream.Write(bytesBuff, 0, bytesBuff.Length);
                        //cStream.Close();
                    }

                    inText = Convert.ToBase64String(mStream.ToArray());
                }
            }
            return inText;
        }
       
        /// <summary>
        /// Decrypt the given string using the given key
        /// </summary>
        /// <param name="cryptTxt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string Decrypt(string cryptTxt, string key)
        {
            cryptTxt = cryptTxt.Replace(" ", "+");
            byte[] bytesBuff = Convert.FromBase64String(cryptTxt);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cStream.Write(bytesBuff, 0, bytesBuff.Length);
                        //cStream.Close();
                    }
                    cryptTxt = Encoding.Unicode.GetString(mStream.ToArray());
                }
            }
            return cryptTxt;
        }
    }
}