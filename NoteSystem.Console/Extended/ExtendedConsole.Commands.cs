using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NoteSystem.ConsoleApp.Extended
{
    public static partial class ExtendedConsole
    {
        private static Dictionary<ConsoleCommand, List<MethodInfo>> _commandHandlers =
            new Dictionary<ConsoleCommand, List<MethodInfo>>();

        public static void ExecuteCommand(ConsoleCommand command)
        {
            if (_commandHandlers.ContainsKey(command))
                _commandHandlers[command].ForEach(methodInfo => methodInfo.Invoke(default, default));

            else Console.WriteLine($"The specified command \"{command}\" was not found," +
                $" perhaps you should mark the static method with the \"{nameof(ConsoleCommandAttribute)}\" attribute and initialize the extended console again"); ;
        }

        public static ConsoleCommandInfo[] GetConsoleCommands()
        {
            var enumFields = typeof(ConsoleCommand).GetFields(BindingFlags.Static | BindingFlags.Public);
            var result = new ConsoleCommandInfo[enumFields.Length];

            for (int i = 0; i < enumFields.Length; i++)
            {
                var description = enumFields[i].GetCustomAttribute<DescriptionAttribute>()?.Description;
                var value = (byte)enumFields[i].GetValue(default);

                result[i] = new ConsoleCommandInfo(description, value);
            }

            return result.OrderBy(info => info.CommandValue).ToArray();
        }

        public static ConsoleCommandInfo[] GetAvailableCommands()
        {
            var i = 0;
            var result = new ConsoleCommandInfo[_commandHandlers.Count];

            foreach (var handler in _commandHandlers)
            {
                var description = handler.Key.GetType()
                    .GetField(handler.Key.ToString(), BindingFlags.Static | BindingFlags.Public)
                    .GetCustomAttribute<DescriptionAttribute>()?.Description;

                var value = (byte)handler.Key;

                result[i++] = new ConsoleCommandInfo(description, value);
            }

            return result.OrderBy(info => info.CommandValue).ToArray();
        }
    }
}
