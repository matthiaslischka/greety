using System;

namespace Greety.HappyZone
{
    public class Greeter
    {
        public void Greet(Action<string> output)
        {
            output("Hello World!");
        }
    }
}