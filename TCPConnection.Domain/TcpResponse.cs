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
        private readonly int _value;
        private readonly string _hostName;
        private readonly int _port;
        private readonly Encoding _encoding;
        public TcpResponse(int value, string hostName, int port, Encoding encoding) {
            _value = value;
            _hostName = hostName;
            _port = port;
            _encoding = encoding;
        }
        public async Task<int?> ExecuteAsync() {
            try {
                using TcpClient _client = new(_hostName, _port);
                using var stream = _client.GetStream();
                using var reader = new StreamReader(stream, _encoding);
                stream.Write(new ReadOnlySpan<byte>(_value.SafeToByteArray()));
                var result = await reader.ReadLineAsync();
                Console.WriteLine($"{_value}\t{result}");
                return result.ToInt();
            }
            catch (Exception) {
                return null;
            }
        }
    }
}
