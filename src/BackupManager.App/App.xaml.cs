using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace BackupManager.App
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IoC.IoCSetup.SetupProvider();

            var isAuthorized = DoLogin();
            if (isAuthorized)
            {
                base.OnStartup(e);
            }

            SetCulture();
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