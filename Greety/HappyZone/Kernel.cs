namespace Greety.HappyZone
{
    public class Kernel
    {
        private readonly IInputOutput _inOut;

        public Kernel(IInputOutput inOut)
        {
            _inOut = inOut;
        }

        public void Run()
        {
            var greeter = new Greeter(_inOut, new[] { "Paul" });

            var name = greeter.AskForName();
            greeter.Greet(name);
        }
    }
}