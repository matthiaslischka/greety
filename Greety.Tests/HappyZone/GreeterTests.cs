using FluentAssertions;
using Greety.HappyZone;
using Xunit;

namespace Greety.Tests.HappyZone
{
    public class GreeterTests
    {
        [Fact]
        public void GreetsWithTheProperText()
        {
            string output = null;
            var greeter = new Greeter();

            // Act
            greeter.Greet(null, msg => output = msg);

            output.Should().Be("Hello World!");
        }
    }
}