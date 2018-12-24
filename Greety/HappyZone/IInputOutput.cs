using System;

namespace Greety.HappyZone
{
    public interface IInputOutput
    {
        Func<string> InputAction { get; }
        Action<string> OutputAction { get; }
    }
}