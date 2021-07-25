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
    public partial class Program {
        public static async Task Main() {
            Console.WriteLine($"start at: {DateTime.Now}");
            int max = 2018;
            var results = Enumerable.Range(1, max)
                .Select(i => new RequestResult(i) { Task = new RepetedTcpResponse(new TcpResponse(i)).ExecuteAsync() })
                .ToList();
            await Task.WhenAll(results.Select(r => r.Task));
            Print(results);
            var badResults = results.Where(r => r.Task.Result is null);
            while (badResults.Any()) {
                badResults.ToList().ForEach(r => r.Task = new RepetedTcpResponse(new TcpResponse(r.Id)).ExecuteAsync());
                await Task.WhenAll(results.Select(r => r.Task));
                Print(results);
            }
        }
        private static void Print(List<RequestResult> results) {
            Console.WriteLine("yet another iteration");
            Console.WriteLine($"start @: {DateTime.Now}");
            results.ForEach(r => Console.WriteLine($"{r.Id}\t{r.Task.Result}"));
        }
    }
}