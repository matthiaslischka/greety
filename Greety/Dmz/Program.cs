using System;
using Greety.HappyZone;

namespace Greety.Dmz
{
    public static class Program
    {
        public static void Main()
        {
            var greeter = new Greeter();
            var name = greeter.AskForName(OutputAction);
            greeter.Greet(name, OutputAction);
        }

        public static Action<string> OutputAction => Console.WriteLine;
    }
}
