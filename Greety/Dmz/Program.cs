using System;
using Greety.HappyZone;

namespace Greety.Dmz
{
    public static class Program
    {
        public static void Main()
        {
            var greeter = new Greeter(OutputAction, InputAction);

            var name = greeter.AskForName();
            greeter.Greet(name);
        }

        public static Func<string> InputAction => Console.ReadLine;

        public static Action<string> OutputAction => Console.WriteLine;
    }
}
