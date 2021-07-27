using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    /// <summary>
    /// runs task specified amount of times
    /// </summary>
    public class RepetedTcpResponse : ICommand<int?> {
        private readonly ICommand<int?> _origin;
        private readonly int _maxTries = 2;
        private int _currentTry = 1;
        public RepetedTcpResponse(ICommand<int?> origin) {
            _origin = origin;
        }
        /// <summary>
        /// returns response from server as asyncronous operation
        /// </summary>
        /// <returns></returns>
        public async Task<int?> ExecuteAsync() {
            int? result = null;
            while (_currentTry <= _maxTries) {
                result = await _origin.ExecuteAsync();
                if (result is null) {
                    _currentTry++;
                    continue;
                }
                break;
            }
            return result;
        }
    }
}
