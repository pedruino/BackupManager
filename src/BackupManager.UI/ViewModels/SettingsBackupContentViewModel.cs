using Avalon.Windows.Dialogs;
using BackupManager.Domain.Enumerations;
using BackupManager.Domain.Interfaces;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;

namespace BackupManager.UI.ViewModels
{
    public class SettingsBackupContentViewModel : BindableBase
    {
        private const string MSG_CHOOSE_BACKUP_DATABASE = "Escolha o arquivo do banco de dados para realizar o backup.";
        private const string MSG_CHOOSE_BACKUP_FOLDER = "Escolha a pasta para realizar o backup.";
        private readonly ISettingsBackupContent _settingsBackupContent;

        public SettingsBackupContentViewModel(ISettingsBackupContent settingsBackupContent)
        {
            _settingsBackupContent = settingsBackupContent;
        }

        public ContentType ContentType
        {
            get => _settingsBackupContent.ContentType;
            set
            {
                _settingsBackupContent.ContentType = value;
                FullPath = null;
                RaisePropertyChanged();
            }
        }

        public string FullPath
        {
            get => _settingsBackupContent.FullPath;
            set
            {
                _settingsBackupContent.FullPath = value;
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get => _settingsBackupContent.Password;
            set => _settingsBackupContent.Password = value;
        }

        public string User
        {
            get => _settingsBackupContent.User;
            set => _settingsBackupContent.User = value;
        }

        private DelegateCommand _selectFolderOrFilePathCommand;

        public DelegateCommand SelectFolderOrFilePathCommand =>
            _selectFolderOrFilePathCommand ?? (_selectFolderOrFilePathCommand = new DelegateCommand(() =>
            {
                if (ContentType == ContentType.Database)
                {
                    var dialog = new OpenFileDialog() { Title = MSG_CHOOSE_BACKUP_DATABASE };
                    if (dialog.ShowDialog() ?? false)
                    {
                        FullPath = dialog.FileName;
                    }
                }
                else
                {
                    var dialog = new FolderBrowserDialog() { Title = MSG_CHOOSE_BACKUP_FOLDER, BrowseFiles = false };

                    if (dialog.ShowDialog() ?? false)
                    {
                        FullPath = dialog.SelectedPath;
                    }
                }
            }));
    }
}