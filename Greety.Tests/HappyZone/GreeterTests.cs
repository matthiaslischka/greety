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
        public void Greet_WithANameContainedInTheListOfWellKnownNames_GreetsFriendly()
        {
            var greeter = CreateGreeter(nameValidator:new LiteralNameValidator("Susi"));

            // Act
            greeter.Greet("Susi");

            _output.Should().Be("Hello World, Susi!");
        }

        [Fact]
        public void Greet_WithAnEmptyListOfWellKnownNames_GreetsUnfriendly()
        {
            var greeter = CreateGreeter();

            // Act
            greeter.Greet("Some other name");

            _output.Should().Be("I don't know you, stranger.");
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
            return CreateGreeter(new LiteralNameValidator());
        }

        private Greeter CreateGreeter(INameValidator nameValidator)
        {
            var inOut = new GenericInputOutput(() => _input, msg => _output = msg);
            return new Greeter(inOut, nameValidator);
        }
    }
}