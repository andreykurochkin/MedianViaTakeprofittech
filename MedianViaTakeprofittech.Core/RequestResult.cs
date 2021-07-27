using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    /// <summary>
    /// Poco to store results from server
    /// </summary>
    public class RequestResult {
        public int Id { get; }
        public RequestResult(int id) => Id = id;
        public Task<int?> Task { get; set; }
        public ICommand<int?> Command { get; set; }
    }
}
