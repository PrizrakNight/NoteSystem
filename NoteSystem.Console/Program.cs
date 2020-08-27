using NoteSystem.BLL;
using System;

namespace NoteSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccessor.SetDefaultConfiguration();
            UserInterface.PrintMainMenu();
            Console.ReadKey();
        }
    }
}
