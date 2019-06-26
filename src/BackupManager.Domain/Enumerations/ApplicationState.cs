using BackupManager.Domain.Infra.Globalization;

namespace BackupManager.Domain.Enumerations
{
    public enum ApplicationState
    {
        [LocalizedDescriptionKey("ApplicationState_Running")]
        Running,

        [LocalizedDescriptionKey("ApplicationState_Paused")]
        Paused,
    }
}