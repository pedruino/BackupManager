using System;
using BackupManager.Domain.Globalization;

namespace BackupManager.Domain.Enumerations
{
    [Flags]
    public enum DayOfWeek
    {
        [LocalizedDescriptionKey("DayOfWeek_Monday")]
        Monday = 1 << 0,

        [LocalizedDescriptionKey("DayOfWeek_Tuesday")]
        Tuesday = 1 << 1,

        [LocalizedDescriptionKey("DayOfWeek_Wednesday")]
        Wednesday = 1 << 2,

        [LocalizedDescriptionKey("DayOfWeek_Thursday")]
        Thursday = 1 << 3,

        [LocalizedDescriptionKey("DayOfWeek_Friday")]
        Friday = 1 << 4,

        [LocalizedDescriptionKey("DayOfWeek_Saturday")]
        Saturday = 1 << 5,

        [LocalizedDescriptionKey("DayOfWeek_Sunday")]
        Sunday = 1 << 6,

        //[LocalizedDescriptionKey("DayOfWeek_All")]
        //All = 1 << 7,
    }
}