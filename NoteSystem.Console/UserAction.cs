using System;

namespace NoteSystem.ConsoleApp
{
    public struct UserAction
    {
        public readonly string Name;

        private readonly Action _action;

        public UserAction(string name, Action action)
        {
            Name = name;
            _action = action;
        }

        public void Execute() => _action.Invoke();
    }
}
