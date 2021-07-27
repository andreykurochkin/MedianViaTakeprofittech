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
        internal static class Factory {
            internal static ICommand<List<RequestResult>> CreateExchangeProvider(IEnumerable<int> range) =>
                new TcpExchangeProviderWithUnderlinedCheck(
                    range,
                    Settings.Host,
                    Settings.Port,
                    Settings.Encoding
                );
            internal static IEnumerable<IEnumerable<int>> CreateRanges() {
                yield return Enumerable.Range(Settings.RequestStartValue, 500);
                yield return Enumerable.Range(501, 500);
                yield return Enumerable.Range(1001, 500);
                yield return Enumerable.Range(1501, 500);
                yield return Enumerable.Range(2001, Settings.RequestEndValue - 2000);
            }
        }
        public static async Task Main() {
            Console.WriteLine($"start @: {DateTime.Now}");
            var requestResults = new List<RequestResult>();
            foreach (IEnumerable<int> range in Factory.CreateRanges()) {
                Console.WriteLine($"start @: {DateTime.Now}, for <{range.First()}:{range.Last()}>");
                var results = await Factory.CreateExchangeProvider(range).ExecuteAsync();
                results.ForEach(result => Console.WriteLine($"{result.Id};{result.Task.Result.Value}"));
                requestResults.AddRange(results);
                Console.WriteLine($"done @: {DateTime.Now}");
            }
            var median = new Median(requestResults.Select(r=>r.Task.Result.Value).ToArray());
            Console.WriteLine($"median : {median.ToDecimal()}");
        }
    }
}