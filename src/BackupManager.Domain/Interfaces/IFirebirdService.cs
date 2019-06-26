using System.IO.Abstractions;

namespace BackupManager.Domain.Interfaces
{
    public interface IFirebirdService
    {
        IFileInfo GenerateFbk(ISettingsBackupContent settingsBackupContent);
    }
}