using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    /// <summary>
    /// provides responses from server
    /// </summary>
    public class TcpExchangeProvider : ICommand<List<RequestResult>> {
        private readonly IEnumerable<int> _range;
        private readonly string _hostName;
        private readonly int _port;
        private readonly Encoding _encoding;
        private List<RequestResult> _results;
        public TcpExchangeProvider(IEnumerable<int> range, string hostName, int port, Encoding encoding) =>
            (_range, _hostName, _port, _encoding) = (range, hostName, port, encoding);
        /// <summary>
        /// returns responses from server
        /// </summary>
        /// <returns></returns>
        public async Task<List<RequestResult>> ExecuteAsync() {
            _results = _range.Select(i =>
                new RequestResult(i) {
                    Task = new RepetedTcpResponse(new TcpResponse(i, _hostName, _port, _encoding)).ExecuteAsync()
                }
            ).ToList();
            await Task.WhenAll(_results.Select(r => r.Task));
            var badResultsQuery = _results.Where(r => r.Task.Result is null);
            while (badResultsQuery.Any()) {
                var badResults = badResultsQuery.ToList();
                badResults.ForEach(r => r.Task = new RepetedTcpResponse(new TcpResponse(r.Id, _hostName, _port, _encoding)).ExecuteAsync());
                await Task.WhenAll(_results.Select(r => r.Task));
            }
            return _results;
        }
    }
}
