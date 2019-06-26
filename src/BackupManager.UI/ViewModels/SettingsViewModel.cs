using System.ComponentModel;
using BackupManager.Domain.Interfaces;
using Prism.Mvvm;

namespace BackupManager.UI.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        public SettingsViewModel(ISettingsService settingsService, ISettings settings)
        {
            PropertyChangedEventHandler SettingsOnPropertyChanged() => delegate
            {
                settingsService.Save(settings);
            };

            settings.Customer.PropertyChanged += SettingsOnPropertyChanged();
            settings.BackupContent.PropertyChanged += SettingsOnPropertyChanged();
            settings.Ftp.PropertyChanged += SettingsOnPropertyChanged();
            settings.Schedule.PropertyChanged += SettingsOnPropertyChanged();

            SettingsCustomerVM = new SettingsCustomerViewModel(settings.Customer);
            SettingsBackupContentVM = new SettingsBackupContentViewModel(settings.BackupContent);
            SettingsFtpVM = new SettingsFtpViewModel(settings.Ftp);
            SettingsScheduleVM = new SettingsScheduleViewModel(settings.Schedule);
        }

        public SettingsCustomerViewModel SettingsCustomerVM { get; }
        public SettingsBackupContentViewModel SettingsBackupContentVM { get; }
        public SettingsFtpViewModel SettingsFtpVM { get; }
        public SettingsScheduleViewModel SettingsScheduleVM { get; }
    }
}