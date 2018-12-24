using System;

namespace Greety.HappyZone
{
    public interface IInputOutput
    {
        Func<string> ReadFromInput { get; }
        Action<string> WriteToOutput { get; }
    }
}