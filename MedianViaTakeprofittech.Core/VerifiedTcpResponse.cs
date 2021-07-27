using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedianViaTakeprofittech.Application {
    /// <summary>
    /// returns verified value from the server
    /// </summary>
    public class VerifiedTcpResponse : ICommand<int?> {
        private readonly ICommand<int?> _origin;
        private readonly int _maxTries = 3;
        private int _currentTry = 1;
        private readonly List<int> _values = new();
        public VerifiedTcpResponse(ICommand<int?> origin) {
            _origin = origin;
        }
        public async Task<int?> ExecuteAsync() {
            int? result = await _origin.ExecuteAsync();
            while (_currentTry <= _maxTries) {
                _currentTry++;
                if (result is null) {
                    continue;
                }
                if (!_values.Contains(result.Value)) {
                    _values.Add(result.Value);
                    continue;
                }
                break;
            }
            return result;
        }
    }
}
