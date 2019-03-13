using System;
using System.ComponentModel;
using BackupManager.Domain.Enumerations;
using BackupManager.Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prism.Mvvm;
using DayOfWeek = BackupManager.Domain.Enumerations.DayOfWeek;

namespace BackupManager.Domain.Entities
{
    public sealed class SettingsSchedule : BindableBase, ISettingsSchedule
    {
        private DateTime _time;
        private int _repetition;
        private DateInterval _frequency;
        private DayOfWeek _daysOfWeek;

        public DateTime Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        public int Repetition
        {
            get => _repetition;
            set => SetProperty(ref _repetition, value);
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public DateInterval Frequency
        {
            get => _frequency;
            set => SetProperty(ref _frequency, value);
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public DayOfWeek DaysOfWeek
        {
            get => _daysOfWeek;
            set => SetProperty(ref _daysOfWeek, value);
        }
    }
}