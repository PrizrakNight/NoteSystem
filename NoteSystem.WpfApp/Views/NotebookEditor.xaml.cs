using System.Windows;

namespace NoteSystem.WpfApp
{
    /// <summary>
    /// Логика взаимодействия для NotebookEditor.xaml
    /// </summary>
    public partial class NotebookEditor : Window
    {
        public NotebookEditor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
