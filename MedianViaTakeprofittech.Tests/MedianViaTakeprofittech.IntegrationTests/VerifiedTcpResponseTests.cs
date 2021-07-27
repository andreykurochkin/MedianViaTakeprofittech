using MedianViaTakeprofittech.Application;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Linq;
using Xunit.Abstractions;
namespace MedianViaTakeprofittech.IntegrationTests {
    public class VerifiedTcpResponseTests {
        private readonly ITestOutputHelper _output;
        public VerifiedTcpResponseTests(ITestOutputHelper output) {
            _output = output;
        }
        [Theory]
        [InlineData(3, 8162618)]
        [InlineData(528, 7806741)]
        public void VerifiedTcpResponseInstance_ShouldReturnSameResults_WhenQueryingServerMultipleTimes(int id, int expected) {
            var tasks = new Task<int?>[] {
                new VerifiedTcpResponse(
                    new TcpResponse(id, Settings.Host, Settings.Port, Settings.Encoding)
                ).ExecuteAsync(),
                new RepetedTcpResponse(
                    new TcpResponse(id, Settings.Host, Settings.Port, Settings.Encoding)
                ).ExecuteAsync(),new RepetedTcpResponse(
                    new TcpResponse(id, Settings.Host, Settings.Port, Settings.Encoding)
                ).ExecuteAsync()
            };
            Task.WhenAll(tasks);
            var values = tasks.Select(t => t.Result.Value).ToList();
            values.TrueForAll(v => v == expected).Should().BeTrue();
            values.ForEach(v => _output.WriteLine(v.ToString()));
        }
    }
}
