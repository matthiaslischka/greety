using System;
using System.IO;
using FluentAssertions;
using Greety.Dmz;
using Xunit;

namespace Greety.Tests.DMZ
{
    public class OutputActionTests
    {
        [Fact]
        public void CallingTheReturnedAction_WritesTheParameterToConsoleOut()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var outputAction = Program.OutputAction;

            // Act
            outputAction("The message");

            var output = stringWriter.ToString();
            output.Should().StartWith("The message");
        }
    }
}