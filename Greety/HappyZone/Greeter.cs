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
            var message = _wellKnownNames.Contains(name)
                ? $"Hello World, {name}!"
                : "I don't know you, stranger.";
            _inOut.WriteToOutput(message);
        }

        public string AskForName()
        {
            _inOut.WriteToOutput("What's your name? ");
            return _inOut.ReadFromInput();
        }
    }
}