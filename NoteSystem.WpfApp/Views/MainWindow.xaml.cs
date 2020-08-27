using NoteSystem.BLL;
using NoteSystem.BLL.Dto;
using NoteSystem.WpfApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace NoteSystem.WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var editor = new NotebookEditor();

            if(editor.ShowDialog() == true && DataContext is MainWindowVM mainWindowVM)
            {
                var newNotebookDto = new NotebookDto
                {
                    Changed = DateTime.Now,
                    Created = DateTime.Now,
                    Notes = new ObservableCollection<NoteDto>(),
                    Name = ((NotebookVM)editor.DataContext).Name
                };

                mainWindowVM.Notebooks.Add(newNotebookDto);
                DataAccessor.AddNotebook(newNotebookDto, mainWindowVM.SelectedSource);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowVM mainWindowVM)
            {
                DataAccessor.RemoveNotebook(mainWindowVM.SelectedNotebook, mainWindowVM.SelectedSource);
                mainWindowVM.Notebooks.Remove(mainWindowVM.SelectedNotebook);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(DataContext is MainWindowVM mainWindowVM)
            {
                var newNote = new NoteDto
                {
                    Changed = DateTime.Now,
                    Created = DateTime.Now,
                    Name = "New Note",
                    Text = "New Note Text"
                };

                mainWindowVM.AddNewNote(newNote);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowVM mainWindowVM && mainWindowVM.SelectedNotebook != default)
                DataAccessor.UpdateNotebook(mainWindowVM.SelectedNotebook, mainWindowVM.SelectedSource);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowVM mainWindowVM && mainWindowVM.SelectedNotebook != default)
            {
                mainWindowVM.SelectedNotebook.Notes.Remove(mainWindowVM.SelectedNote);
                DataAccessor.UpdateNotebook(mainWindowVM.SelectedNotebook, mainWindowVM.SelectedSource);
            }
        }
    }
}
