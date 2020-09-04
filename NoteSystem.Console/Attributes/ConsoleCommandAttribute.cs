using System;

namespace NoteSystem.ConsoleApp
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConsoleCommandAttribute : Attribute
    {
        public readonly ConsoleCommand ConsoleCommand;

        public ConsoleCommandAttribute(ConsoleCommand consoleCommand)
        {
            ConsoleCommand = consoleCommand;
        }
    }
}
