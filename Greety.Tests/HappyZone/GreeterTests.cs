using FluentAssertions;
using Greety.HappyZone;
using Xunit;

namespace Greety.Tests.HappyZone
{
    public class GreeterTests
    {
        private string _input;
        private string _output;

        [Fact]
        public void Greet_GreetsWithTheProperText()
        {
            var greeter = CreateGreeter();

            // Act
            greeter.Greet("Susi");

            _output.Should().Be("Hello World, Susi!");
        }

        [Fact]
        public void AskForName_PromptsForTheName()
        {
            var greeter = CreateGreeter();

            // Act
            greeter.AskForName();

            _output.Should().Be("What's your name? ");
        }

        [Fact]
        public void AskForName_ReturnsTheInputText()
        {
            _input = "Susi";
            var greeter = CreateGreeter();

            // Act
            var name = greeter.AskForName();

            name.Should().Be("Susi");
        }

        private Greeter CreateGreeter()
        {
            var inOut = new GenericInputOutput(() => _input, msg => _output = msg);
            return new Greeter(inOut);
        }
    }
}