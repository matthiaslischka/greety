using System;
using System.IO;
using FluentAssertions;
using Greety.Dmz;
using Xunit;

namespace Greety.Tests.DMZ
{
    [Collection("Tests depending on System.Console")]
    public class SmokeTests
    {
        [Fact]
        public void GreetsToTheConsole()
        {
            var stringWriter = new StringWriter();
            var stringReader = new StringReader("Paul");
            Console.SetIn(stringReader);
            Console.SetOut(stringWriter);

            // Act
            Program.Main();

            var output = stringWriter.ToString();
            output.Should().Contain("Paul");
        }
    }
}