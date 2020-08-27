using System;

namespace NoteSystem.BLL.Dto
{
    public class NoteDto
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
    }
}
