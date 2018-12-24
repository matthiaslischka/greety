using System;

namespace Greety.HappyZone
{
    public class InputOutput
    {
        public InputOutput(Func<string> inputAction, Action<string> outputAction)
        {
            InputAction = inputAction;
            OutputAction = outputAction;
        }

        public Func<string> InputAction { get; }
        public Action<string> OutputAction { get; }
    }
}