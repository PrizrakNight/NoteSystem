namespace NoteSystem.WpfApp.ViewModels
{
    public class NoteVM : ViewModel
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

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        private string _text;
    }
}
