using System;
using Greety.HappyZone;

namespace Greety.Dmz
{
    public class ConsoleInputOutput : IInputOutput
    {
        public Func<string> ReadFromInput { get; } = Console.ReadLine;
        public Action<string> WriteToOutput { get; } = Console.WriteLine;
    }
}