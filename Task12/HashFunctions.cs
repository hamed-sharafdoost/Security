using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    public class HashFunctions
    {
        public string HashWithMD5(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            MD5 md5 = MD5.Create();
            byte[] hashData = md5.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
            {
                sb.Append(hashData[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public string HashWithSHA256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256 sha256 = SHA256.Create();
            byte[] hashData = sha256.ComputeHash(bytes);
            StringBuilder sb2 = new StringBuilder();
            for (int j = 0; j < hashData.Length; j++)
            {
                sb2.Append(j.ToString("X2"));
            }
            return sb2.ToString();
        }
    }
}
