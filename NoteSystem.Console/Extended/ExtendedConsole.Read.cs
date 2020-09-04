using NoteSystem.BLL;
using NoteSystem.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteSystem.ConsoleApp.Extended
{
    public static partial class ExtendedConsole
    {
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
            catch (Exception ex)
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
