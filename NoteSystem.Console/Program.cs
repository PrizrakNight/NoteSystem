using NoteSystem.BLL;
using NoteSystem.BLL.Dto;
using System;

namespace NoteSystem.ConsoleApp
{
    [ConsoleCommandContainer]
    internal class Program
    {
        private static bool _exited;
        private static DataSourceType _sourceType = DataSourceType.Xml;

        private static void Main(string[] args)
        {
            DataAccessor.SetDefaultConfiguration();
            ExtendedConsole.Initialize();

            PrintAvailableCommands();

            while (!_exited)
            {
                if (ExtendedConsole.TryReadCommand(out ConsoleCommand command))
                    ExtendedConsole.ExecuteCommand(command);
            }

            Console.WriteLine("The program has successfully completed its work");
        }

        [ConsoleCommand(ConsoleCommand.CreateNotebook)]
        private static void CreateNewNotebook()
        {
            DataAccessor.AddNotebook(ExtendedConsole.ReadNotebook(), _sourceType);
            Console.WriteLine("Notebook created!");
        }

        [ConsoleCommand(ConsoleCommand.DisplayNotebooksWithNotes)]
        private static void DisplayNotebooks()
        {
            var notebooks = DataAccessor.GetNotebooks(_sourceType);

            for (int i = 0; i < notebooks.Length; i++)
            {
                Console.WriteLine(notebooks[i].Name);

                for (int j = 0; j < notebooks[i].Notes.Count; j++)
                    Console.WriteLine($" - {notebooks[i].Notes[j].Name}, {notebooks[i].Notes[j].Text}");
            }
        }

        [ConsoleCommand(ConsoleCommand.DeleteNotebook)]
        private static void DeleteNotebook()
        {
            var existingNotebooks = DataAccessor.GetNotebooks(_sourceType);

            PrintNotebooks(existingNotebooks);

            if (ExtendedConsole.TryReadInt32WithMessage("Enter the index of the notebook you are delete", out int index))
            {
                try
                {
                    var notebook = existingNotebooks[index];

                    DataAccessor.RemoveNotebook(notebook, _sourceType);
                    Console.WriteLine("Notebook successfully deleted");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The index you specified is out of bounds for existing notebooks, please try again");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        [ConsoleCommand(ConsoleCommand.CreateNoteIntoNotebook)]
        private static void AddNoteIntoNotebook()
        {
            var existingNotebooks = DataAccessor.GetNotebooks(_sourceType);

            PrintNotebooks(existingNotebooks);

            if(ExtendedConsole.TryReadInt32WithMessage("Enter the index of the notebook you are interested in", out int index))
            {
                try
                {
                    var notebook = existingNotebooks[index];
                    var note = ExtendedConsole.ReadNote();

                    notebook.Notes.Add(note);

                    DataAccessor.UpdateNotebook(notebook, _sourceType);
                    Console.WriteLine("Note has been successfully added to notebook");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The index you specified is out of bounds for existing notebooks, please try again");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void PrintNotebooks(NotebookDto[] notebooks)
        {
            for (int i = 0; i < notebooks.Length; i++)
                Console.WriteLine($"[{i}]: " + notebooks[i].Name);
        }

        private static void PrintNotes(NotebookDto notebook)
        {
            for (int i = 0; i < notebook.Notes.Count; i++)
                Console.WriteLine($"[{i}]: { notebook.Notes[i].Name}, { notebook.Notes[i].Text}");
        }

        [ConsoleCommand(ConsoleCommand.CloseProgram)]
        private static void ExitProgram() => _exited = true;

        private static void PrintAvailableCommands()
        {
            var commandInfos = ExtendedConsole.GetConsoleCommands();

            for (int i = 0; i < commandInfos.Length; i++)
                Console.WriteLine(commandInfos[i]);
        }
    }
}
