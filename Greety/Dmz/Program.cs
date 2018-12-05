using System;

namespace Greety.Dmz
{
    public static class Program
    {
        public static void Main()
        {
            Action<string> output = Console.WriteLine;
            Greet(output);
        }

        // TODO: Move this to the HappyZone!
        public static void Greet(Action<string> output)
        {
            output("Hello World!");
        }
    }
}
