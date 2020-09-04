using NoteSystem.BLL;
using NoteSystem.BLL.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NoteSystem.ConsoleApp
{
    public static class ExtendedConsole
    {
        private static Dictionary<ConsoleCommand, List<MethodInfo>> _commandHandlers =
            new Dictionary<ConsoleCommand, List<MethodInfo>>();

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

            return result;
        }

        public static NotebookDto EditNotebook(NotebookDto notebook)
        {
            notebook.Changed = DateTime.Now;
            notebook.Name = ReadLineWithMessage("Enter a name for the notebook");

            return notebook;
        }

        public static NoteDto EditNote(NoteDto note)
        {
            note.Changed = DateTime.Now;
            note.Name = ReadLineWithMessage("Enter a name for the note");
            note.Text = ReadLineWithMessage("Enter your note text");

            return note;
        }

        public static NotebookDto ReadNotebook()
        {
            return CreatableFactory.NowWith<NotebookDto>(notebook =>
            {
                notebook.Name = ReadLineWithMessage("Enter a name for the notebook");
            });
        }

        public static NoteDto ReadNote()
        {
            return CreatableFactory.NowWith<NoteDto>(note =>
            {
                note.Name = ReadLineWithMessage("Enter a name for the note");
                note.Text = ReadLineWithMessage("Enter your note text");
            });
        }

        public static ConsoleCommand ReadCommand()
        {
            var commandNumber = int.Parse(Console.ReadLine());

            return (ConsoleCommand)commandNumber;
        }

        public static bool TryReadCommand(out ConsoleCommand command)
        {
            try
            {
                command = ReadCommand();

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            command = ConsoleCommand.CloseProgram;

            return false;
        }

        public static string ReadLineWithMessage(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }

        public static bool TryReadInt32(out int int32)
        {
            if (int.TryParse(Console.ReadLine(), out int32))
                return true;

            return false;
        }

        public static bool TryReadInt32WithMessage(string message, out int int32)
        {
            if (int.TryParse(ReadLineWithMessage(message), out int32))
                return true;

            return false;
        }
    }
}
