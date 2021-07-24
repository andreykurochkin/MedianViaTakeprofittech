using System;
using System.Text;
using Xunit;
using TCPConnection.Domain;
using FluentAssertions;
using System.Threading.Tasks;

namespace TCPConnection.DomainTests {
    public class ExtensionsTest {
        [Theory]
        [InlineData(1, new byte[] { 49, 10})]
        [InlineData(11, new byte[] { 49, 49, 10})]
        public void ToByteArray_ShouldReturnValueWithLineFeedByte_WhenDataIsValid(int value, byte[] expected) {
            Func<int, byte[]> sut = Extensions.ToByteArray<int>;

            var result = sut(value);

            result.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public void ToByteArray_ShouldReturnEmptyArray_WhenDataIsNull() {
            Func<string, byte[]> sut = Extensions.ToByteArray<string>;

            var result = sut(null);

            result.Should().BeEmpty();
        }
        [Theory]
        [InlineData("..	8162348	................", "8162348")]
        [InlineData("...........	8003730	......", "8003730")]
        [InlineData("...........	-8003730	......", "-8003730")]
        public void ToIntegers_ShouldReturnExpectedString_WhenDataIsValid(string input, string expected) {
            Func<string, string> sut = Extensions.ToIntegers;

            var result = sut(input);

            result.Should().Be(expected);
        }
        [Fact]
        public void ToIntegers_ShouldReturnEmptyString_WhenDataIsEmptyString() {
            Func<string, string> sut = Extensions.ToIntegers;

            var result = sut(string.Empty);

            result.Should().BeEmpty();
        }
        [Fact]
        public void ToIntegers_ShouldThrowAnError_WhenDataIsNull() {
            Action act = () => Extensions.ToIntegers(null);
            
            act.Should().Throw<Exception>();
        }
        [Theory]
        [InlineData("..	8162348	................", 8162348)]
        [InlineData("...........	8003730	......", 8003730)]
        [InlineData("...........	-8003730	......", -8003730)]
        public void ToInt_ShouldReturnExpectedInteger_WhenDataIsValid(string input, int expected) {
            Func<string, int> sut = Extensions.ToInt;

            var result = sut(input);

            result.Should().Be(expected);
        }
        [Fact]
        public void ToInt_ShouldThrow_WhenDataIsNullOrEmpty() {
            Action act = () => Extensions.ToInt(string.Empty);
            act.Should().Throw<Exception>();
            act = () => Extensions.ToInt(null);
            act.Should().Throw<Exception>();
        }
    }
}
