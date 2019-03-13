using BackupManager.Domain.Enumerations;
using BackupManager.Domain.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using DayOfWeek = BackupManager.Domain.Enumerations.DayOfWeek;

namespace BackupManager.App.ViewModels
{
    public class SettingsScheduleViewModel : BindableBase
    {
        private readonly ISettingsSchedule _settingsSchedule;
        private DelegateCommand<DayOfWeek?> _toggleDayOfWeekCommand;

        public SettingsScheduleViewModel(ISettingsSchedule settingsSchedule)
        {
            _settingsSchedule = settingsSchedule;
        }

        public DateTime Time
        {
            get => _settingsSchedule.Time;
            set => _settingsSchedule.Time = value;
        }

        public int Repetition
        {
            get => _settingsSchedule.Repetition;
            set => _settingsSchedule.Repetition = value;
        }

        public DateInterval Frequency
        {
            get => _settingsSchedule.Frequency;
            set
            {
                _settingsSchedule.Frequency = value;
                RaisePropertyChanged();
            }
        }

        public DayOfWeek DaysOfWeek => _settingsSchedule.DaysOfWeek;

        public DelegateCommand<DayOfWeek?> ToggleDayOfWeekCommand =>
            _toggleDayOfWeekCommand ?? (_toggleDayOfWeekCommand = new DelegateCommand<DayOfWeek?>((dayOfWeek) =>
            {
                if (dayOfWeek.HasValue)
                    _settingsSchedule.DaysOfWeek ^= dayOfWeek.Value;
            }));
    }
}