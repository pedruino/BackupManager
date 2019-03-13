namespace BackupManager.Domain.Interfaces
{
    public interface ISettings
    {
        ISettingsCustomer Customer { get; }
        ISettingsBackupContent BackupContent { get; }
        ISettingsFtp Ftp { get; }
        ISettingsSchedule Schedule { get; }
    }
}