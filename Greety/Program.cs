using System;

namespace Greety
{
    public static class Program
    {
        public static void Main()
        {
            Action<string> output = Console.WriteLine;
            output("Hello World!");
        }
    }
}
