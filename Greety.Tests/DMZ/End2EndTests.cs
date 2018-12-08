using System;
using System.IO;
using FluentAssertions;
using Greety.Dmz;
using Xunit;

namespace Greety.Tests.DMZ
{
    [Collection("Tests depending on System.Console")]
    public class End2EndTests
    {
        [Fact]
        public void GreetsToTheConsole()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            Program.Main();

            var output = stringWriter.ToString();
            output.Should().Be("Hello World!\r\n");
        }
    }
}