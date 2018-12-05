using System;
using Greety.HappyZone;

namespace Greety.Dmz
{
    public static class Program
    {
        public static void Main()
        {
            Action<string> output = Console.WriteLine;

            var greeter = new Greeter();
            greeter.Greet(output);
        }
    }
}
