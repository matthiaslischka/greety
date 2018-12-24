using System;
using Greety.HappyZone;

namespace Greety.Tests.HappyZone
{
    internal class GenericInputOutput : IInputOutput
    {
        public GenericInputOutput(Func<string> inputAction, Action<string> outputAction)
        {
            ReadFromInput = inputAction;
            WriteToOutput = outputAction;
        }

        public Func<string> ReadFromInput { get; }
        public Action<string> WriteToOutput { get; }
    }
}