using BackupManager.Domain.Interfaces;
using Prism.Mvvm;

namespace BackupManager.Domain.Entities
{
    public sealed class SettingsFtp : BindableBase, ISettingsFtp
    {
        private string _host;
        private string _password;
        private string _username;

        public SettingsFtp(string host, string username, string password)
        {
            Host = host;
            Username = username;
            Password = password;
        }

        internal SettingsFtp()
        {
        }

        public string Host
        {
            get => _host;
            set => SetProperty(ref _host, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
    }
}