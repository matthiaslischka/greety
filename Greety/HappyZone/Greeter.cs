namespace Greety.HappyZone
{
    public class Greeter
    {
        private readonly IInputOutput _inOut;
        private readonly INameValidator _nameValidator;

        public Greeter(IInputOutput inOut, INameValidator nameValidator)
        {
            _inOut = inOut;
            _nameValidator = nameValidator;
        }

        public void Greet(string name)
        {
            var message = _nameValidator.IsValid(name)
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