using System;
using Greety.HappyZone;

namespace Greety.Dmz
{
    public static class Program
    {
        public static readonly ConsoleInputOutput _consoleInOut = new ConsoleInputOutput();

        public static void Main()
        {
            var greeter = new Greeter(_consoleInOut);

            var name = greeter.AskForName();
            greeter.Greet(name);
        }
    }
}
