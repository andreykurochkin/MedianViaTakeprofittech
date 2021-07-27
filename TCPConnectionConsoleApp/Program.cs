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
        private static TcpExchangeProviderWithUnderlinedCheck CreateTcpExchangeProvider(IEnumerable<int> range) {
            return new TcpExchangeProviderWithUnderlinedCheck(
                    range,
                    Settings.Host,
                    Settings.Port,
                    Settings.Encoding
                );
        }
        private static IEnumerable<IEnumerable<int>> CreateRanges() {
            yield return Enumerable.Range(Settings.RequestStartValue, 500);
            yield return Enumerable.Range(501, 500);
            yield return Enumerable.Range(1001, 500);
            yield return Enumerable.Range(1501, 500);
            yield return Enumerable.Range(2001, Settings.RequestEndValue - 2000);
        }
        private static async Task<List<RequestResult>> GetResults(IEnumerable<int> range) {
            var dataProvider = CreateTcpExchangeProvider(range);
            await dataProvider.GetValues();
            return dataProvider._results;
        }
        public static async Task Main() {
            Console.WriteLine($"start @: {DateTime.Now}");
            var requestResults = new List<RequestResult>();
            foreach (IEnumerable<int> range in CreateRanges()) {
                Console.WriteLine($"start @: {DateTime.Now}, for <{range.First()}:{range.Last()}>");
                var results = await GetResults(range);
                results.ForEach(result => Console.WriteLine($"{result.Id};{result.Task.Result.Value}"));
                requestResults.AddRange(results);
                Console.WriteLine($"done @: {DateTime.Now}");
            }
            var median = new Median(requestResults.Select(r=>r.Task.Result.Value).ToArray());
            Console.WriteLine($"median : {median.ToDecimal()}");
        }
    }
}