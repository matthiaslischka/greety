namespace Greety.HappyZone
{
    public class Greeter
    {
        private readonly IInputOutput _inOut;

        public Greeter(IInputOutput inOut)
        {
            _inOut = inOut;
        }

        public void Greet(string name)
        {
            _inOut.WriteToOutput($"Hello World, {name}!");
        }

        public string AskForName()
        {
            _inOut.WriteToOutput("What's your name? ");
            return _inOut.ReadFromInput();
        }
    }
}