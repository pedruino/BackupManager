using BackupManager.Domain.Infra.Globalization;

namespace BackupManager.Domain.Enumerations
{
    public enum ContentType
    {
        [LocalizedDescriptionKey("ContentType_Directory")]
        Directory = 0,
        [LocalizedDescriptionKey("ContentType_Database")]
        Database = 1,
    }
}