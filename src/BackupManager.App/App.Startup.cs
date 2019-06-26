using BackupManager.Domain.Interfaces;
using CommonServiceLocator;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

namespace BackupManager.App
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IoC.IoCSetup.SetupProvider();

            //Current.MainWindow = new MainWindow();

            var isAuthorized = DoLogin();
            if (isAuthorized)
            {
                base.OnStartup(e);
            }

            SetCulture();

            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");

            // Start scheduler for tasks
            StartScheduler(ServiceLocator.Current.GetInstance<ISettings>());
        }
    }
}