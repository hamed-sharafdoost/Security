using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            TConfiguration config = null;
            TTransport trans = new TTlsSocketTransport("127.0.0.1", 2020,config,6000,GetCertificate());
            TBinaryProtocol proto = new TBinaryProtocol(trans);
            HelloSvc.Client client = new HelloSvc.Client(proto);
            trans.OpenAsync();
            string result = client.GetMessage("hiiiiiii").Result;
            Console.WriteLine("Received from server :"+result);
            Console.ReadKey();
        }
        private static X509Certificate2 GetCertificate()
        {
            // due to files location in net core better to take certs from top folder
            var certFile = GetCertPath(Directory.GetParent(Directory.GetCurrentDirectory()));
            return new X509Certificate2(certFile, "1380");
        }

        private static string GetCertPath(DirectoryInfo di, int maxCount = 6)
        {
            var topDir = di;
            var certFile =
                topDir.EnumerateFiles("ThriftTls.pfx", SearchOption.AllDirectories)
                    .FirstOrDefault();
            if (certFile == null)
            {
                if (maxCount == 0)
                    throw new FileNotFoundException("Cannot find file in directories");
                return GetCertPath(di.Parent, maxCount - 1);
            }

            return certFile.FullName;
        }
    }
}
