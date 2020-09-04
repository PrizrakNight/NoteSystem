using NoteSystem.BLL.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace NoteSystem.BLL.Dto
{
    public class NotebookDto : ICreatable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }

        public ObservableCollection<NoteDto> Notes { get; set; }
    }
}
