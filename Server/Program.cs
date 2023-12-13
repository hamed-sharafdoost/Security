using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Transport;
using Thrift.Server;
using System.Threading;
using Thrift.Transport.Server;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport.Client;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            int port = 2020;
            CancellationToken token = CancellationToken.None;
            HelloHandler helloHandler = new HelloHandler();
            HelloSvc.AsyncProcessor proc = new HelloSvc.AsyncProcessor(helloHandler);
            TConfiguration config = new TConfiguration();
            TServerTransport trans = new TTlsServerSocketTransport(port, config, GetCertificate());
            TServer server =new TThreadPoolAsyncServer(proc, trans);
            Console.WriteLine("Server is running on port :" + port);
            server.ServeAsync(token);
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
