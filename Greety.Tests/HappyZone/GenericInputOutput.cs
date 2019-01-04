using System;
using Greety.HappyZone;

namespace Greety.Tests.HappyZone
{
    internal class GenericInputOutput : IInputOutput
    {
        private readonly Func<string> _readFromInputAction;
        private readonly Action<string> _writeToOutputAction;

        public GenericInputOutput(Func<string> inputAction, Action<string> outputAction)
        {
            _readFromInputAction = inputAction;
            _writeToOutputAction = outputAction;
        }

        public string ReadFromInput() => _readFromInputAction();
        public void WriteToOutput(string message) => _writeToOutputAction(message);
    }
}