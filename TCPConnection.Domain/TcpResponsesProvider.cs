using System.Linq;
using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    /// <summary>
    /// provides responses from 
    /// </summary>
    public class TcpResponsesProvider : IResponsesProvider {
        public async Task<int[]> GetValues() {
            int max = 2018;
            var results = Enumerable.Range(1, max)
                .Select(i => new RequestResult(i) { Task = new RepetedTcpResponse(new TcpResponse(i)).ExecuteAsync() })
                .ToList();
            await Task.WhenAll(results.Select(r => r.Task));
            var badResults = results.Where(r => r.Task.Result is null);
            while (badResults.Any()) {
                badResults.ToList().ForEach(r => r.Task = new RepetedTcpResponse(new TcpResponse(r.Id)).ExecuteAsync());
                await Task.WhenAll(results.Select(r => r.Task));
            }
            return results.Select(r => r.Task.Result.Value).ToArray();
        }
    }
}
