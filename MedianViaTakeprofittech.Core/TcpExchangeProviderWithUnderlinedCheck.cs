using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    public class TcpExchangeProviderWithUnderlinedCheck : ICommand<List<RequestResult>> {
        private readonly IEnumerable<int> _range;
        private readonly string _hostName;
        private readonly int _port;
        private readonly Encoding _encoding;
        private List<RequestResult> _results;
        public TcpExchangeProviderWithUnderlinedCheck(IEnumerable<int> range, string hostName, int port, Encoding encoding) =>
            (_range, _hostName, _port, _encoding) = (range, hostName, port, encoding);
        /// <summary>
        /// returns responses from server
        /// </summary>
        /// <returns></returns>
        public async Task<List<RequestResult>> ExecuteAsync() {
            _results = _range.Select(i =>
                new RequestResult(i) {
                    Task = new VerifiedTcpResponse(new TcpResponse(i, _hostName, _port, _encoding)).ExecuteAsync()
                }
            ).ToList();
            await Task.WhenAll(_results.Select(r => r.Task));
            var badResultsQuery = _results.Where(r => r.Task.Result is null);
            while (badResultsQuery.Any()) {
                ProgressLog(badResultsQuery);
                badResultsQuery.ToList()
                    .ForEach(r => r.Task = new VerifiedTcpResponse(new TcpResponse(r.Id, _hostName, _port, _encoding)).ExecuteAsync());
                await Task.WhenAll(_results.Select(r => r.Task));
            }
            return _results;
        }
        private void ProgressLog(IEnumerable<RequestResult> query) {
            var badResults = query.Where(r => r.Task.Result is null).ToList();
            var total = _range.Count();
            Console.WriteLine("===================================================");
            Console.WriteLine($"start @: {DateTime.Now}, for <{_range.First()}:{_range.Last()}>");
            Console.WriteLine($"progress:\t{100 * (total - badResults.Count) / total} %");
            Console.WriteLine($"done:\t{total - badResults.Count} ");
            Console.WriteLine($"total:\t{total}");
            Console.WriteLine("===================================================");
        }
    }
}
