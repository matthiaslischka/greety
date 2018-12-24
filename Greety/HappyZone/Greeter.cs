using System;

namespace Greety.HappyZone
{
    public class Greeter
    {
        public void Greet(string name, Action<string> output)
        {
            output("Hello World!");
        }

        public string AskForName()
        {
            throw new NotImplementedException();
        }
    }
}