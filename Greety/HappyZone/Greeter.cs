using System;

namespace Greety.HappyZone
{
    public class Greeter
    {
        private readonly Action<string> _output;
        private readonly Func<string> _input;

        public Greeter(Action<string> outputAction, Func<string> inputAction)
        {
            _output = outputAction;
            _input = inputAction;
        }

        public void Greet(string name)
        {
            _output($"Hello World, {name}!");
        }

        public string AskForName()
        {
            _output("What's your name? ");
            return _input();
        }
    }
}