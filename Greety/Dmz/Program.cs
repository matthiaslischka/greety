using System;
using Greety.HappyZone;

namespace Greety.Dmz
{
    public static class Program
    {
        public static void Main()
        {
            var output = OutputAction;

            var greeter = new Greeter();
            greeter.Greet(output);
        }

        public static Action<string> OutputAction => Console.WriteLine;
    }
}
