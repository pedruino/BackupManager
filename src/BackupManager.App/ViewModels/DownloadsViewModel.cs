using System.Collections.ObjectModel;
using Avalon.Windows.Dialogs;
using BackupManager.Domain.Entities;
using BackupManager.Domain.Interfaces;
using Prism.Commands;

namespace BackupManager.App.ViewModels
{
    public class DownloadsViewModel
    {
        private const string MSG_CHOOSE_DOWNLOAD_FOLDER = "Escolha a pasta para salvar o arquivo de backup.";
        private readonly IBackupService _backupService;
        private DelegateCommand<BackupFile> _restoreCommand;
        private DelegateCommand<BackupFile> _saveAsCommand;

        public DownloadsViewModel(IBackupService backupService)
        {
            _backupService = backupService;
            Backups = new ObservableCollection<BackupFile>(_backupService.GetBackupFiles());
        }

        public ObservableCollection<BackupFile> Backups { get; }

        public DelegateCommand<BackupFile> RestoreCommand
        {
            get
            {
                return _restoreCommand ?? (_restoreCommand = new DelegateCommand<BackupFile>((backupFile) =>
                {
                    //TODO: download to temp directory
                    //TODO: call ino-backup-service with args (gbak_path, db_path, db_user, db_pass, others_args)
                        //TODO: unzip files to defined db extension
                        //TODO: start gbak process
                        //TODO: show busy progress
                    //TODO: start gbak process
                }));
            }
        }
        public DelegateCommand<BackupFile> SaveAsCommand
        {
            get
            {
                return _saveAsCommand ?? (_saveAsCommand = new DelegateCommand<BackupFile>((backupFile) =>
                {
                    var dialog = new FolderBrowserDialog() { Title = MSG_CHOOSE_DOWNLOAD_FOLDER };

                    if (dialog.ShowDialog() ?? false)
                    {
                        _backupService.DownloadFile(backupFile, localPath: dialog.SelectedPath);
                    }
                }));
            }
        }
    }
}