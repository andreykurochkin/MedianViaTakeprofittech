using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace MedianViaTakeprofittech.Application {
    /// <summary>
    /// retrieves response from server
    /// </summary>
    public class TcpResponse : ICommand<int?> {
        private static readonly Encoding _koi8r = CodePagesEncodingProvider.Instance.GetEncoding("koi8-r");
        private readonly int _value;
        public TcpResponse(int value) {
            _value = value;
        }
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
