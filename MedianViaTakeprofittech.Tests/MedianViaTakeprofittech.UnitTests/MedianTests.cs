using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedianViaTakeprofittech.Application;
using Xunit;

namespace TCPConnection.DomainTests {
    public class MedianTests {
        private Median _sut;
        [Theory]
        [InlineData(new int[] { 1 }, 1.0)]
        [InlineData(new int[] { 2, 2 }, 2.0)]
        [InlineData(new int[] { 16, 12, 99, 95, 18, 87, 10 }, 18.0)]
        [InlineData(new int[] { 9, 1, 0, 2, 3, 4, 6, 8, 7, 10, 5 }, 5)]
        [InlineData(new int[] { 1,3,5,7}, 4)]
        public void ToDecimal_ShouldReturnExpectedValue_WhenDataIsValid(int[] array, decimal expected) {
            _sut = new(array);

            var result = _sut.ToDecimal();

            result.Should().Be(expected);
        }
    }
}
