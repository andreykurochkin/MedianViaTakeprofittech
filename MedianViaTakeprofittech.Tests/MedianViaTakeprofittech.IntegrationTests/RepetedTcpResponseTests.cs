using MedianViaTakeprofittech.Application;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Linq;
using Xunit.Abstractions;
namespace MedianViaTakeprofittech.IntegrationTests {
    public class RepetedTcpResponseTests {
        private readonly ITestOutputHelper _output;
        public RepetedTcpResponseTests(ITestOutputHelper output) {
            _output = output;
        }
        [Theory]
        [InlineData(3)]
        [InlineData(528)]
        public void RepetedTcpResponseInstanse_ShouldReturnSameResults_WhenQueryingServerMultipleTimes(int id) {
            var tasks = new Task<int?>[] {
                new RepetedTcpResponse(
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
            values.TrueForAll(v => v == values[0]).Should().BeTrue();
            values.ForEach(v => _output.WriteLine(v.ToString()));
        }
    }
}
