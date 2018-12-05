using FluentAssertions;
using Xunit;

namespace Greety.Tests
{
    public class GreetTests
    {
        [Fact]
        public void GreetsWithTheProperText()
        {
            string output = null;

            // Act
            Program.Greet(msg => output = msg);

            output.Should().Be("Hello World!");
        }
    }
}