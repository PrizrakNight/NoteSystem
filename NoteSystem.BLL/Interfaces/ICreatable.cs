using System;

namespace NoteSystem.BLL.Interfaces
{
    public interface ICreatable
    {
        DateTime Created { get; set; }
        DateTime Changed { get; set; }
    }
}
