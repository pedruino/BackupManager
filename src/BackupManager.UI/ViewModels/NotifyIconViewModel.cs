using System.Windows;
using System.Windows.Input;
using BackupManager.UI.Views;
using CommonServiceLocator;
using Prism.Commands;
using Prism.Mvvm;

namespace BackupManager.UI.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel : BindableBase
    {
        private bool _canHide = false;

        public NotifyIconViewModel()
        {
            //RaisePropertyChanged(nameof(CanHide));
        }
        public bool CanHide
        {
            get => _canHide;
            set => SetProperty(ref _canHide, value);
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand(() => Application.Current.Shutdown());
            }
        }

        /// <summary>
        /// Hides the main window. This command is only enabled if a window is open.
        /// </summary>
        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand(() =>
                    {
                        if (Application.Current.MainWindow == null) return;

                        Application.Current.MainWindow.Hide();

                        //Application.Current.PreferencesWindow.Close();
                        CanHide = Application.Current.MainWindow.IsVisible;
                    }, () => CanHide)
                    .ObservesProperty(() => CanHide);
            }
        }

        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand(() =>
                    {
                        if (Application.Current.MainWindow == null)
                        {
                            Application.Current.MainWindow = new MainWindow()
                            {
                                DataContext = ServiceLocator.Current.GetInstance<MainWindowViewModel>()
                            };
                        }

                        if (!Application.Current.MainWindow.IsActive)
                        {
                            Application.Current.MainWindow.Show();
                            CanHide = true;
                        }
                    });
            }
        }
    }
}