using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MedianViaTakeprofittech.Application;
using System.Linq;
using System.Collections.Generic;

namespace TCPConnectionConsoleApp {
    public partial class Program {
        public static async Task Main() {
            var ints = await new TcpResponsesProvider().GetValues();
            var result = new Median(ints).ToDecimal();
            Console.WriteLine(result);
        }

    }
}