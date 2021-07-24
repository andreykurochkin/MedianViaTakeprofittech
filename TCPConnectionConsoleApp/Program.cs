using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCPConnection.Domain;
using System.Linq;
using System.Collections.Generic;

namespace TCPConnectionConsoleApp {
    public class Program {
        //private const string message = "Greetings\n";
        //private const string path = "C:\\Code\\TCPConnectionConsoleApp\\TCPConnectionConsoleApp\\task.txt";
        //private static readonly Encoding _koi8r = System.Text.CodePagesEncodingProvider.Instance.GetEncoding("koi8-r");
        //private static readonly IPAddress _ipAddr = IPAddress.Parse(host);
        //private static readonly IPEndPoint _endPoint = new(_ipAddr, port);
        //private static readonly byte[] foo = Encoding.ASCII.GetBytes(message);
        public static async Task Main() {
            Console.WriteLine($"start at: {DateTime.Now}");
            int max = 2018;
            var tasks = new List<Task<int?>>();
            var range = Enumerable.Range(1, max).ToList();
            //range.ForEach(i => tasks.Add(new TcpResponse(i).InvokeWithTries()));
            //range.ForEach(i => tasks.Add(new IntInvoker(i).Invoke()));
            range.ForEach(i => tasks.Add(
                    new RepetedTcpResponse(
                        new TcpResponse(i)
                    ).ExecuteAsync()
                )
            );

            await Task.WhenAll(tasks.ToArray());
            range.ForEach(i => Console.WriteLine($"{i}\t{tasks[i - 1].Result}"));
            Console.WriteLine($"stop at: {DateTime.Now}");
        }
    }
}