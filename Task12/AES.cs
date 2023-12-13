using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    public class AES
    {
        private byte[] salt = Encoding.Unicode.GetBytes("7Days");
        private const int iteration = 2000;
        public string Encrypt(string text,string pass)
        {
            byte[] textBytes = Encoding.Unicode.GetBytes(text);
            byte[] encryptedBytes;
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, iteration);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);
            using(var ms = new MemoryStream())
            {
                using(var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(textBytes, 0, textBytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);
        }
        public string Decrypt(string cryptoText,string pass)
        {
            byte[] cryptoBytes = Convert.FromBase64String(cryptoText);
            byte[] text;
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(pass,salt, iteration);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);
            using (var ms = new MemoryStream())
            {
                using(var cs = new CryptoStream(ms,aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes,0,cryptoBytes.Length);
                }
                text = ms.ToArray();
            }
            return Encoding.Unicode.GetString(text);
        }
    }
}
