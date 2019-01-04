namespace Greety.HappyZone
{
    public interface IInputOutput
    {
        string ReadFromInput();
        void WriteToOutput(string message);
    }
}