using NoteSystem.BLL;
using NoteSystem.BLL.Dto;
using System.Collections.ObjectModel;

namespace NoteSystem.WpfApp.ViewModels
{
    public class MainWindowVM : ViewModel
    {
        public DataSourceType[] AllSources
        {
            get
            {
                if (_allTypes == default)
                    _allTypes = DataAccessor.Configuration.SourceTypes;

                return _allTypes;
            }
        }

        public DataSourceType SelectedSource
        {
            get => _selectedSource;
            set
            {
                _selectedSource = value;

                Notebooks = new ObservableCollection<NotebookDto>(DataAccessor.GetNotebooks(_selectedSource));

                OnPropertyChanged();
            }
        }

        public ObservableCollection<NotebookDto> Notebooks
        {
            get => _notebooks;
            set
            {
                _notebooks = value;
                OnPropertyChanged();
            }
        }

        public NotebookDto SelectedNotebook
        {
            get => _selectedNotebook;
            set
            {
                _selectedNotebook = value;
                OnPropertyChanged();
            }
        }

        public NoteDto SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<NotebookDto> _notebooks = new ObservableCollection<NotebookDto>();
        private NotebookDto _selectedNotebook;
        private NoteDto _selectedNote;

        private DataSourceType[] _allTypes;
        private DataSourceType _selectedSource = DataSourceType.InMemory;

        public void AddNewNote(NoteDto noteDto)
        {
            if(SelectedNotebook != default)
            {
                SelectedNotebook.Notes.Add(noteDto);
                DataAccessor.UpdateNotebook(SelectedNotebook, SelectedSource);
            }    
        }
    }
}
