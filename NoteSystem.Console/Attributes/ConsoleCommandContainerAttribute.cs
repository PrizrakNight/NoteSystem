using System;

namespace NoteSystem.ConsoleApp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class ConsoleCommandContainerAttribute : Attribute
    {
    }
}
