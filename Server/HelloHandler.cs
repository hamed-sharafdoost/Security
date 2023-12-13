using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class HelloHandler : HelloSvc.IAsync
    {
        public Task<string> GetMessage(string name, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Recieved from client : " + name);
            string msg = "Hello" + name;
            return Task.FromResult(msg);
        }
    }
}
