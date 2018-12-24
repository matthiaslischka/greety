using FluentAssertions;
using Greety.HappyZone;
using Xunit;

namespace Greety.Tests.HappyZone
{
    public class GreeterTests
    {
        [Fact]
        public void Greet_GreetsWithTheProperText()
        {
            string output = null;
            var greeter = new Greeter();

            // Act
            greeter.Greet("Susi", msg => output = msg);

            output.Should().Be("Hello World, Susi!");
        }

        [Fact]
        public void AskForName_PromptsForTheName()
        {
            string output = null;
            var greeter = new Greeter();

            // Act
            greeter.AskForName(msg => output = msg, () => "");

            output.Should().Be("What's your name? ");
        }

        [Fact]
        public void AskForName_ReturnsTheInputText()
        {
            string input = "Susi";
            string output = null;
            var greeter = new Greeter();

            // Act
            var name = greeter.AskForName(msg => output = msg, () => input);

            name.Should().Be("Susi");
        }
    }
}