using System;
using Greety.HappyZone;

namespace Greety.Dmz
{
    public static class Program
    {
        public static void Main()
        {
            var greeter = new Greeter();
            var name = greeter.AskForName(OutputAction, InputAction);
            greeter.Greet(name, OutputAction);
        }

        public static Func<string> InputAction => Console.ReadLine;

        public static Action<string> OutputAction => Console.WriteLine;
    }
}
