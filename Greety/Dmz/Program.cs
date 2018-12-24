using Greety.HappyZone;

namespace Greety.Dmz
{
    public static class Program
    {
        public static void Main()
        {
            var consoleInOut = new ConsoleInputOutput();
            var greeter = new Greeter(consoleInOut, new[]{"Paul"});

            var name = greeter.AskForName();
            greeter.Greet(name);
        }
    }
}
