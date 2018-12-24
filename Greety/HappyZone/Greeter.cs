using System;

namespace Greety.HappyZone
{
    public class Greeter
    {
        public void Greet(string name, Action<string> output)
        {
            output($"Hello World, {name}!");
        }

        public string AskForName(Action<string> output)
        {
            output("What's your name? ");
            return null;
        }
    }
}