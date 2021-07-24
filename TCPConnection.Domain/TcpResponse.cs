using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TCPConnection.Domain {
    public interface ICommand<TResult> {
        public Task<TResult> ExecuteAsync();
    }
    /// <summary>
    /// runs task specified amount of times
    /// </summary>
    public class RepetedTcpResponse : ICommand<int?> {
        private readonly ICommand<int?> _origin;
        private readonly int _maxTries = 3;
        private int _currentTry = 1;
        public RepetedTcpResponse(ICommand<int?> origin) {
            _origin = origin;
        }
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
    /// <summary>
    /// returns integer from server
    /// </summary>
    public class TcpResponse : ICommand<int?> {
        private static readonly Encoding _koi8r = CodePagesEncodingProvider.Instance.GetEncoding("koi8-r");
        private readonly int _value;
        //private readonly int _result;
        //private readonly TcpClient _client;
        //private int _currentTry = 1;
        //private int _maxTries = 3;
        public TcpResponse(int value) {
            _value = value;
        }
        //public async Task<int?> InvokeWithTries() {
        //    int? result = null;
        //    while (_currentTry <= _maxTries) {
        //        result = await Invoke();
        //        if (result is null) {
        //            _currentTry++;
        //            continue;
        //        }
        //        break;
        //    }
        //    return result;
        //}
        //public int? Tries(int start, int max) {
        //    int? result = null;
        //    int? current = start;
        //    while(current <= max) {
        //        current++;
        //    }
        //    result = current;
        //    return result;
        //}
        public async Task<int?> ExecuteAsync() {
            try {
                using TcpClient _client = new("88.212.241.115", 2012);
                using var stream = _client.GetStream();
                using var reader = new StreamReader(stream, _koi8r);
                stream.Write(new ReadOnlySpan<byte>(_value.SafeToByteArray()));
                var result = await reader.ReadLineAsync();
                return result.ToInt();
            }
            catch (Exception) {
                return null;
            }
        }
    }
}
