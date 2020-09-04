using NoteSystem.BLL.Dto;
using System;

namespace NoteSystem.ConsoleApp.Extended
{
    public static partial class ExtendedConsole
    {
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
    }
}
