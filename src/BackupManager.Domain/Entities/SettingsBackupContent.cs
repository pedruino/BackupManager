using BackupManager.Domain.Enumerations;
using BackupManager.Domain.Interfaces;
using Prism.Mvvm;

namespace BackupManager.Domain.Entities
{
    public sealed class SettingsBackupContent : BindableBase, ISettingsBackupContent
    {
        public const string DEFAULT_PASSWORD = "masterkey";
        public const string DEFAULT_USER = "sysdba";
        private ContentType _contentType;
        private string _fullPath;
        private string _password;
        private string _user;

        public ContentType ContentType
        {
            get => _contentType;
            set => SetProperty(ref _contentType, value);
        }

        public string FullPath
        {
            get => _fullPath;
            set => SetProperty(ref _fullPath, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
    }
}