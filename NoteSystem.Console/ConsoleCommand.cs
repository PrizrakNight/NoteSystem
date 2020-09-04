using System.ComponentModel;

namespace NoteSystem.ConsoleApp
{
    public enum ConsoleCommand : byte
    {
        [Description("Create a new notebook")]
        CreateNotebook = 1,

        [Description("Add new note to notebook")]
        CreateNoteIntoNotebook,

        [Description("Delete note from notebook")]
        DeleteNoteFromNotebook,

        [Description("Delete notebook")]
        DeleteNotebook,

        [Description("Display existing notebook with notes")]
        DisplayNotebooksWithNotes,

        [Description("Edit notebook")]
        EditNotebook,

        [Description("Edit note in notebook")]
        EditNoteInNotebook,

        [Description("Closing the program")]
        CloseProgram
    }
}
