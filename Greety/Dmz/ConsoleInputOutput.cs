using System;
using Greety.HappyZone;

namespace Greety.Dmz
{
    public class ConsoleInputOutput : IInputOutput
    {
        public string ReadFromInput() => Console.ReadLine();

        public Action<string> WriteToOutput { get; } = Console.WriteLine;
    }
}