using BackupManager.Domain.Interfaces;
using Newtonsoft.Json;

namespace BackupManager.Domain.Entities
{
    public sealed class Settings : ISettings
    {
        [JsonConstructor]
        public Settings(SettingsCustomer customer, SettingsBackupContent backupContent, SettingsFtp ftp, SettingsSchedule schedule)
        {
            Customer = customer ?? new SettingsCustomer();
            BackupContent = backupContent ?? new SettingsBackupContent();
            Ftp = ftp ?? new SettingsFtp();
            Schedule = schedule ?? new SettingsSchedule();
        }

        internal Settings() : this(null, null, null, null)
        {
        }

        public ISettingsCustomer Customer { get; }
        public ISettingsBackupContent BackupContent { get; }
        public ISettingsFtp Ftp { get; }
        public ISettingsSchedule Schedule { get; }
    }
}