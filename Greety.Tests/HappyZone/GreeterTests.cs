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
            var greeter = new Greeter(new InputOutput(() => "", msg => output = msg));

            // Act
            greeter.Greet("Susi");

            output.Should().Be("Hello World, Susi!");
        }

        [Fact]
        public void AskForName_PromptsForTheName()
        {
            string output = null;
            var greeter = new Greeter(new InputOutput(() => "", msg => output = msg));

            // Act
            greeter.AskForName();

            output.Should().Be("What's your name? ");
        }

        [Fact]
        public void AskForName_ReturnsTheInputText()
        {
            string input = "Susi";
            var greeter = new Greeter(new InputOutput(() => input, msg => { }));

            // Act
            var name = greeter.AskForName();

            name.Should().Be("Susi");
        }
    }
}