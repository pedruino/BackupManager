using BackupManager.Domain.Globalization;

namespace BackupManager.Domain.Enumerations
{
    public enum DateInterval
    {
        [LocalizedDescriptionKey("DateInterval_Day")]
        Day = 0,
        [LocalizedDescriptionKey("DateInterval_Week")]
        Week = 1,
        [LocalizedDescriptionKey("DateInterval_Month")]
        Month = 2,
        [LocalizedDescriptionKey("DateInterval_Year")]
        Year = 3
    }
}