using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TCPConnection.Domain {
    public class IntInvoker {
        private static readonly Encoding _koi8r = System.Text.CodePagesEncodingProvider.Instance.GetEncoding("koi8-r");
        private int _value;
        private int _result;
        private TcpClient _client;
        public IntInvoker(int value) {
            _value = value;
        }
        public int Invoke() {
            _result = _value;
            return _value;
            //try {
            //    using TcpClient _client = new TcpClient("88.212.241.115", 2012);
            //    var stream = _client.GetStream();
            //    using var reader = new StreamReader(stream, _koi8r);
            //    stream.Write(new ReadOnlySpan<byte>(value.SafeToByteArray()));
            //    var result = await reader.ReadLineAsync();
            //    return result.ToInt();
            //}
            //catch (Exception) {
            //    return null;
            //}
        }
    }
}
