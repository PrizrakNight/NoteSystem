using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NoteSystem.ConsoleApp.Extended
{
    public static partial class ExtendedConsole
    {
        public static void Initialize()
        {
            var commandContainers = typeof(ExtendedConsole).Assembly.GetTypes()
                .Where(type => type.GetCustomAttribute<ConsoleCommandContainerAttribute>() != default)
                .ToArray();

            for (int i = 0; i < commandContainers.Length; i++)
            {
                var commandHandlers = commandContainers[i]
                    .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                    .Select(method => Tuple.Create(method.GetCustomAttribute<ConsoleCommandAttribute>(), method))
                    .Where(tuple => tuple.Item1 != default)
                    .ToArray();

                for (int j = 0; j < commandHandlers.Length; j++)
                {
                    var consoleCommand = commandHandlers[j].Item1.ConsoleCommand;
                    var methodInfo = commandHandlers[j].Item2;

                    if (!_commandHandlers.ContainsKey(consoleCommand))
                        _commandHandlers.Add(consoleCommand, new List<MethodInfo>(new MethodInfo[] { methodInfo }));

                    else _commandHandlers[consoleCommand].Add(methodInfo);
                }
            }
        }
    }
}
