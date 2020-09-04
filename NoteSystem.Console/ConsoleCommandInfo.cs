namespace NoteSystem.ConsoleApp
{
    public struct ConsoleCommandInfo
    {
        public readonly string Text;
        public readonly byte CommandValue;

        public ConsoleCommandInfo(string text, byte commandValue)
        {
            Text = text;
            CommandValue = commandValue;
        }

        public override string ToString()
        {
            return $"{CommandValue}. " + Text;
        }
    }
}
