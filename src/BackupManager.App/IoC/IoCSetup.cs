using BackupManager.Domain.Interfaces;
using BackupManager.Domain.Services;
using CommonServiceLocator;
using Ninject;
using System;
using System.IO;

namespace BackupManager.App.IoC
{
    public static class IoCSetup
    {
        private static StandardKernel Kernel { get; } = new StandardKernel();

        public static void SetupProvider()
        {
            Kernel
                .Bind<ISettingsService>()
                .To<SettingsService>()
                .InSingletonScope()
                .WithConstructorArgument("path", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "BackupManager"))
                .WithConstructorArgument("settingsFileName", "settings.json");

            Kernel.Bind<ISettings>()
                  .ToMethod((ctx) => ctx.Kernel.TryGet<ISettingsService>()?.Load())
                  .InSingletonScope();

            Kernel
                .Bind<IBackupService>()
                .To<BackupService>();

            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(Kernel));
        }
    }
}