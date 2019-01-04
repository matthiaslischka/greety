using System;

namespace Greety.HappyZone
{
    public interface IInputOutput
    {
        string ReadFromInput();
        Action<string> WriteToOutput { get; }
    }
}