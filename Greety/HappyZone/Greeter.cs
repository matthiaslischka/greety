using System.Linq;

namespace Greety.HappyZone
{
    public class Greeter
    {
        private readonly IInputOutput _inOut;
        private readonly string[] _wellKnownNames;

        public Greeter(IInputOutput inOut, string[] wellKnownNames)
        {
            _inOut = inOut;
            _wellKnownNames = wellKnownNames;
        }

        public void Greet(string name)
        {
            if (!_wellKnownNames.Contains(name))
                _inOut.WriteToOutput("I don't know you, stranger.");
            else
                _inOut.WriteToOutput($"Hello World, {name}!");
        }

        public string AskForName()
        {
            _inOut.WriteToOutput("What's your name? ");
            return _inOut.ReadFromInput();
        }
    }
}