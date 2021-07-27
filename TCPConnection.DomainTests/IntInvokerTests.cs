using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using MedianViaTakeprofittech.Application;
using System.IO;

namespace TCPConnection.DomainTests {
    public class IntInvokerTests {
        [Fact]
        public void Tries_Should_When() {
            //todo decide whether delete it
            double d = 2.0;
            int i = 1;

            d = (double)i / 2;
            var path = @"C:\Users\kurochkin.andrei\Desktop\test.txt";
            //var strings = File.ReadAllLines(path).Select(s => s.Split(" ").Where(ss => ss != " ").ToList(1]).ToList();

            //var max = 10;
            //var start = 1;
            //var sut = new TcpResponse(0);

            //var result = sut.Tries(start, max);

            //result.Should().Be(10);

        }
    }
}
