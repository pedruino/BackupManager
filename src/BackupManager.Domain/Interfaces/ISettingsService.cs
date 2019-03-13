namespace BackupManager.Domain.Interfaces
{
    public interface ISettingsService
    {
        ISettings Load();

        void Save(ISettings settings);
    }
}