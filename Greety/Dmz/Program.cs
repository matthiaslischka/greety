using System;
using Greety.HappyZone;

namespace Greety.Dmz
{
    public static class Program
    {
        public static void Main()
        {
            var greeter = new Greeter();
            var name = greeter.AskForName(OutputAction, () => Console.ReadLine());
            greeter.Greet(name, OutputAction);
        }

        public static Action<string> OutputAction => Console.WriteLine;
    }
}
