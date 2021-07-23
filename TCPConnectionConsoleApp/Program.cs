using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPConnectionConsoleApp {
    public class Program {
        private const string host = "88.212.241.115";
        private const int port = 2012;
        private const string message = "Greetings\n";
        //private const string path = "C:\\Code\\TCPConnectionConsoleApp\\TCPConnectionConsoleApp\\task.txt";
        private static readonly Encoding _koi8r = System.Text.CodePagesEncodingProvider.Instance.GetEncoding("koi8-r");
        private static readonly IPAddress _ipAddr = IPAddress.Parse(host);
        private static readonly IPEndPoint _endPoint = new(_ipAddr, port);
        private static readonly byte[] foo = Encoding.ASCII.GetBytes(message);
        public async static Task Main() {
            try {
                Console.WriteLine("start...");
                //await ReadBySingleString();
                await ReadNumber();
                Console.WriteLine("end...");
            }
            catch (Exception ex) {
                Console.WriteLine($"erorr: {ex.Message}");
            }
            finally {
                Console.ReadLine();
            
            }
        }
        private static async Task ReadAllStrings() {
            using var tcpClient = new TcpClient();
            tcpClient.Connect(_endPoint);
            var tcpStream = tcpClient.GetStream();
            await tcpStream.WriteAsync(foo);
            var response = new byte[tcpClient.ReceiveBufferSize];
            await tcpStream.ReadAsync(response.AsMemory(0, tcpClient.ReceiveBufferSize));
            var result = _koi8r.GetString(response);
            Console.WriteLine(result);
        }
        private static async Task ReadBySingleString() {
            using var tcpClient = new TcpClient(host, port);
            using var tcpStream = tcpClient.GetStream();
            tcpStream.Write(foo, 0, foo.Length);
            using var streamReader = new StreamReader(tcpStream, _koi8r);
            while (true) {
                try {
                    var result = await streamReader.ReadLineAsync();
                    Console.WriteLine(result);
                }
                catch (Exception) {
                    throw;
                }
            }
        }
        private static async Task ReadNumber() {
            using var tcpClient = new TcpClient(host, port);
            using var tcpStream = tcpClient.GetStream();
            var bytes = Encoding.ASCII.GetBytes("1\n");
            tcpStream.Write(bytes, 0, bytes.Length);
            using var streamReader = new StreamReader(tcpStream, _koi8r);
            var result = await streamReader.ReadLineAsync();
            Console.WriteLine(result) ;
        }
        private static string ToString(int number) => $"{number}\n";
        private static byte[] ToByteArray(string input) => Encoding.ASCII.GetBytes(input);
    }
}
