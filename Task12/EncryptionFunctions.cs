using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12
{
    public class EncryptionFunctions
    {
        public static AES AESAlgorithm { get; } = new AES();
        public static RSA RSAAlgorithm { get; } = new RSA();
    }
}
