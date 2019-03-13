using BackupManager.Domain.Enumerations;

namespace BackupManager.Domain.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static bool HasFlagFast(this DayOfWeek value, DayOfWeek flag)
        {
            return (value & flag) != 0;
        }
    }
}