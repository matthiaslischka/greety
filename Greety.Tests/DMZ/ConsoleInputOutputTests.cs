using System;
using System.IO;
using FluentAssertions;
using Greety.Dmz;
using Xunit;

namespace Greety.Tests.DMZ
{
    [Collection("Tests depending on System.Console")]
    public class ConsoleInputOutputTests
    {
        [Fact]
        public void CallingTheOutputAction_WritesTheParameterToConsoleOut()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var consoleInOut = new ConsoleInputOutput();

            // Act
            consoleInOut.WriteToOutput("The message");

            var output = stringWriter.ToString();
            output.Should().Be("The message\r\n");
        }

        [Fact]
        public void CallingTheInputAction_ReadsTheParameterFromConsoleOut()
        {
            var stringReader = new StringReader("The input");
            Console.SetIn(stringReader);

            var consoleInOut = new ConsoleInputOutput();

            // Act
            var input = consoleInOut.ReadFromInput();

            input.Should().Be("The input");
        }
    }
}