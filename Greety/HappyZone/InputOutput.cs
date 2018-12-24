using System;

namespace Greety.HappyZone
{
    public class InputOutput : IInputOutput
    {
        public InputOutput(Func<string> inputAction, Action<string> outputAction)
        {
            ReadFromInput = inputAction;
            WriteToOutput = outputAction;
        }

        public Func<string> ReadFromInput { get; }
        public Action<string> WriteToOutput { get; }
    }
}