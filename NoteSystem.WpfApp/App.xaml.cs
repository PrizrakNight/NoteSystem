using NoteSystem.BLL;
using System.Windows;

namespace NoteSystem.WpfApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DataAccessor.Configuration = new DataAccessorConfiguration().ConfigureAll("Notebooks");
        }
    }
}
