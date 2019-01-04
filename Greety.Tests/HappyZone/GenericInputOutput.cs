using System;
using Greety.HappyZone;

namespace Greety.Tests.HappyZone
{
    internal class GenericInputOutput : IInputOutput
    {
        private readonly Func<string> _readFromInputAction;

        public GenericInputOutput(Func<string> inputAction, Action<string> outputAction)
        {
            _readFromInputAction = inputAction;
            WriteToOutput = outputAction;
        }

        public string ReadFromInput() => _readFromInputAction();
        public Action<string> WriteToOutput { get; }
    }
}