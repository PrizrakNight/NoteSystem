namespace NoteSystem.WpfApp.ViewModels
{
    public class NotebookVM : ViewModel
    {
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _name;
    }
}
