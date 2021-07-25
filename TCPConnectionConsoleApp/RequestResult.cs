using System.Threading.Tasks;

namespace TCPConnectionConsoleApp {
    public partial class Program {
        /// <summary>
        /// Poco to store values
        /// </summary>
        public class RequestResult {
            public int Id { get; }
            public Task<int?> Task { get; set; }
            public RequestResult(int id) {
                Id = id;
            }
        }
    }
}