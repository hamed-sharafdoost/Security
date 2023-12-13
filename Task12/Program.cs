using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    public class Program
    {
        static void Main(string[] args)
        {
            HashFunctions functions = new HashFunctions();
            Console.WriteLine("Write a sentence to get hash value :");
            string input = Console.ReadLine();
            Console.WriteLine($"\"{input}\" is converted to hash using MD5 algorithm :" + functions.HashWithMD5(input));
            Console.WriteLine($"\"{input}\" is converted to hash using sha256 algorithm :" + functions.HashWithSHA256(input));
            Console.ReadKey();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            Console.WriteLine("Write a sentence to encrypt it :");
            string text = Console.ReadLine();
            Console.WriteLine("Write a password too :");
            string pass = Console.ReadLine();
            string encryptedAes = EncryptionFunctions.AESAlgorithm.Encrypt(text, pass);
            string encryptedRsa = EncryptionFunctions.RSAAlgorithm.Encrypt(text, rsa.ExportParameters(false));
            Console.WriteLine($"{text} is encrypted to {encryptedAes}");
            Console.WriteLine($"{text} is encrypted to {encryptedRsa}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"{encryptedAes} is decrypted to {EncryptionFunctions.AESAlgorithm.Decrypt(encryptedAes, pass)}");
            Console.WriteLine($"{encryptedRsa} is decrypted to {EncryptionFunctions.RSAAlgorithm.Decrypt(encryptedRsa, rsa.ExportParameters(true))}");
            Console.ReadKey();
        }
    }
}
