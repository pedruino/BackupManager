﻿using BackupManager.Domain.Interfaces;
using CommonServiceLocator;
using Prism.Mvvm;

namespace BackupManager.App.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _currentPage;
        private BindableBase _currentVM;

        public MainWindowViewModel() : this(
            ServiceLocator.Current.GetInstance<ISettingsService>(),
            ServiceLocator.Current.GetInstance<ISettings>(),
            ServiceLocator.Current.GetInstance<IBackupService>())
        {
        }

        public MainWindowViewModel(ISettingsService settingsService, ISettings settings, IBackupService backupService)
        {
            SettingsVM = new SettingsViewModel(settingsService, settings);
            DownloadsVM = new DownloadsViewModel(backupService)
            {
                //Backups =
                //{
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123,  DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //    new BackupFile(Guid.NewGuid().ToString(), "uri", 123, DateTime.UtcNow),
                //}
            };
        }

        public string CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public BindableBase CurrentVM
        {
            get => _currentVM;
            set
            {
                _currentVM = value;
                RaisePropertyChanged(nameof(CurrentVM));
            }
        }

        public DownloadsViewModel DownloadsVM { get; }
        public SettingsViewModel SettingsVM { get; }

        
    }
}