using System;
using System.ComponentModel;
using BackupManager.Domain.Enumerations;
using DayOfWeek = BackupManager.Domain.Enumerations.DayOfWeek;

namespace BackupManager.Domain.Interfaces
{
    public interface ISettingsSchedule : INotifyPropertyChanged
    {
        DateTime Time { get; set; }
        int Repetition { get; set; }
        DateInterval Frequency { get; set; }
        DayOfWeek DaysOfWeek { get; set; }
    }
}