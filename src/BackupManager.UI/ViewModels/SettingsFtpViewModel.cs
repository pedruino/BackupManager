using BackupManager.Domain.Interfaces;
using Prism.Mvvm;

namespace BackupManager.UI.ViewModels
{
    public class SettingsFtpViewModel : BindableBase
    {
        private readonly ISettingsFtp _settingsFtp;

        public SettingsFtpViewModel(ISettingsFtp settingsFtp)
        {
            _settingsFtp = settingsFtp;
        }

        public string Host
        {
            get => _settingsFtp.Host;
            set => _settingsFtp.Host = value;
        }

        public string Username
        {
            get => _settingsFtp.Username;
            set => _settingsFtp.Username = value;
        }

        public string Password
        {
            get => _settingsFtp.Password;
            set => _settingsFtp.Password = value;
        }
    }
}