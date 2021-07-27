using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    /// <summary>
    /// Poco for values
    /// </summary>
    public class RequestResult {
        public int Id { get; }
        public RequestResult(int id) => Id = id;
        public Task<int?> Task { get; set; }
        public ICommand<int?> Command { get; set; }
    }
}
