using System;

namespace Greety
{
    public static class Program
    {
        public static void Main()
        {
            Action<string> output = Console.WriteLine;
            Greet(output);
        }

        public static void Greet(Action<string> output)
        {
            output("Hello World!");
        }
    }
}
