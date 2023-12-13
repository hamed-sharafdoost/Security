using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    public class RSA
    {
        public string Encrypt(string text,RSAParameters key)
        {
            byte[] textByte = Encoding.Unicode.GetBytes(text);
            byte[] cipherByte;
            using(RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(key);
                cipherByte = rsa.Encrypt(textByte, false);
            }
            return Convert.ToBase64String(cipherByte);
        }
        public string Decrypt(string cipher,RSAParameters key)
        {
            byte[] cipherByte = Convert.FromBase64String(cipher);
            byte[] textByte;
            using(RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(key);
                textByte = rsa.Decrypt(cipherByte, false);
            }
            return Encoding.UTF8.GetString(textByte);
        }
    }
}
