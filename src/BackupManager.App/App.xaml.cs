using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

namespace BackupManager.App
{
    public partial class App : Application
    {
        private TaskbarIcon _notifyIcon;

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }

        private static bool DoLogin()
        {
            return true;
        }

        private static void SetCulture()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            //FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
            //    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}