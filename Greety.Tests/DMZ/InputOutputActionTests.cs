using System;
using System.IO;
using FluentAssertions;
using Greety.Dmz;
using Xunit;

namespace Greety.Tests.DMZ
{
    [Collection("Tests depending on System.Console")]
    public class InputOutputActionTests
    {
        [Fact]
        public void CallingTheOutputAction_WritesTheParameterToConsoleOut()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            Program._consoleInOut.WriteToOutput("The message");

            var output = stringWriter.ToString();
            output.Should().StartWith("The message");
        }

        [Fact]
        public void CallingTheInputAction_ReadsTheParameterFromConsoleOut()
        {
            var stringReader = new StringReader("The input");
            Console.SetIn(stringReader);

            // Act
            var input = Program._consoleInOut.ReadFromInput();

            input.Should().StartWith("The input");
        }
    }
}