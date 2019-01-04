using Greety.HappyZone;

namespace Greety.Dmz
{
    public static class Program
    {
        public static void Main()
        {
            var consoleInOut = new ConsoleInputOutput();

            var kernel = new Kernel(consoleInOut);
            kernel.Run();
        }
    }
}
