using BackupManager.Domain.Infra.Compression;
using BackupManager.Domain.Interfaces;
using BackupManager.Domain.Services;
using BackupManager.UI.ViewModels;
using CommonServiceLocator;
using Ninject;
using NLog;
using System.Diagnostics;
using System.IO.Abstractions;
using ILogger = NLog.ILogger;

namespace BackupManager.App.IoC
{
    public static class IoCSetup
    {
        private static StandardKernel Kernel { get; } = new StandardKernel(new NinjectSettings()
        {
            LoadExtensions = true
        });

        public static void SetupProvider()
        {
            Kernel.Bind<ILogger>()
                .ToMethod(x =>
                {
                    Debug.Assert(x.Request.ParentContext != null, "x.Request.ParentContext != null");
                    return LogManager.GetCurrentClassLogger(x.Request.ParentContext.Plan?.Type);
                });

            Kernel
                .Bind<IFileSystem>()
                .ToConstant(new FileSystem());

            Kernel
                .Bind<ICompressor>()
                .To<Compressor>()
                .InSingletonScope();

            Kernel
                .Bind<ISettingsService>()
                .To<SettingsService>()
                .InSingletonScope()
                .WithConstructorArgument("path", SettingsService.DEFAULT_PATH)
                .WithConstructorArgument("settingsFileName", SettingsService.DEFAULT_FILENAME);

            Kernel.Bind<ISettings>()
                  .ToMethod((ctx) => ctx.Kernel.TryGet<ISettingsService>()?.Load())
                  .InSingletonScope();

            Kernel
                .Bind<IBackupService>()
                .To<BackupService>();

            Kernel.Bind<IFirebirdService>()
                .To<FirebirdService>()
                .InThreadScope();

            Kernel.Bind<IAuthenticationService>()
                .To<AuthenticationService>()
                .InSingletonScope();

            Kernel.Bind<ApplicationService>()
                .ToSelf()
                .InSingletonScope();

            Kernel.Bind<MainWindowViewModel>()
                .ToConstant(new MainWindowViewModel(Kernel.Get<IAuthenticationService>(), Kernel.Get<ApplicationService>()));

            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(Kernel));
        }
    }
}