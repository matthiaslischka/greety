using System;

namespace Greety.HappyZone
{
    public class Greeter
    {
        private readonly Action<string> _output;
        private readonly Func<string> _input;

        public Greeter(InputOutput inputOutput)
        {
            _output = inputOutput.OutputAction;
            _input = inputOutput.InputAction;
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