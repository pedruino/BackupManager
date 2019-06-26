using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BackupManager.UI.Views
{
    public partial class PreferencesWindow : Window
    {
        public PreferencesWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as FrameworkElement).UpdateLayout();
        }
    }
}