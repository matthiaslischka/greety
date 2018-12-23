using System;
using Greety.HappyZone;

namespace Greety.Dmz
{
    public static class Program
    {
        public static void Main()
        {
            var greeter = new Greeter();
            greeter.Greet(OutputAction);
        }

        public static Action<string> OutputAction => Console.WriteLine;
    }
}
