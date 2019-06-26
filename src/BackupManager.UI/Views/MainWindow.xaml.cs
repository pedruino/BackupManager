using System.Windows;
using System.Windows.Controls;

namespace BackupManager.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Hide();
            var preferences = new PreferencesWindow().ShowDialog();
            Application.Current.MainWindow.Show();
        }
    }
}
