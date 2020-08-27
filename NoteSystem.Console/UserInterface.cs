using System;
using System.Collections.Generic;

namespace NoteSystem.ConsoleApp
{
    public static class UserInterface
    {
        public static readonly Dictionary<ActionType, UserAction[]> Actions = new Dictionary<ActionType, UserAction[]>
        {
            {
                ActionType.Creation, new UserAction[]
                {
                    new UserAction("Create a notebook", PrintNotebookCreation),
                    new UserAction("Create a note", PrintNoteCreation)
                }
            },
            {
                ActionType.Change, new UserAction[]
                {
                    new UserAction("Change notebook", PrintNotebookChange),
                    new UserAction("Change note", PrintNoteChange)
                }
            },
            {
                ActionType.Removal, new UserAction[]
                {
                    new UserAction("Delete notebook", PrintNotebookDelete),
                    new UserAction("Delete note", PrintNoteDelete)
                }
            },
            {
                ActionType.Display, new UserAction[]
                {
                    new UserAction("Show existing notebooks", DisplayNotebooks),
                    new UserAction("Show all existing notes", DisplayAllNotes),
                    new UserAction("Show notebook", DisplaySelectedNote),
                    new UserAction("Show note", DisplaySelectedNote)
                }
            },
            {
                ActionType.Other, new UserAction[]
                {
                    new UserAction("Change data source", PrintChangeDataSource),
                }
            }
        };

        private static void PrintChangeDataSource()
        {
            throw new NotImplementedException();
        }

        public static void PrintMainMenu()
        {
            Console.WriteLine("Welcome to the note system!");
            Console.WriteLine("Select the action of interest: ");
            Console.WriteLine("Usage example: [section name] [action index]");

            PrintSections();

            foreach (var userAction in Actions)
            {
                var actionIndex = 0;
                Console.WriteLine($"{userAction.Key}");

                foreach (var userActionValue in userAction.Value)
                    Console.WriteLine($" [{actionIndex++}]: " + userActionValue.Name);
            }
        }

        private static void PrintSections()
        {
            var allActions = (ActionType[])Enum.GetValues(typeof(ActionType));

            for (int i = 0; i < allActions.Length; i++)
                Console.WriteLine($"{allActions[i].ToString().ToLower()} - {allActions[i]} section");
        }

        private static void DisplaySelectedNote()
        {
            throw new NotImplementedException();
        }

        private static void DisplayAllNotes()
        {
            throw new NotImplementedException();
        }

        private static void DisplayNotebooks()
        {
            throw new NotImplementedException();
        }

        private static void PrintNoteDelete()
        {
            throw new NotImplementedException();
        }

        private static void PrintNotebookDelete()
        {
            throw new NotImplementedException();
        }

        private static void PrintNoteChange()
        {
            throw new NotImplementedException();
        }

        private static void PrintNotebookChange()
        {
            throw new NotImplementedException();
        }

        private static void PrintNoteCreation()
        {
            throw new NotImplementedException();
        }

        private static void PrintMainMenuActions()
        {
            Console.WriteLine("");
        }

        private static void PrintNotebookCreation()
        {

        }
    }
}
