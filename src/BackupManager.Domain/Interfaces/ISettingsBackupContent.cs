﻿using System.ComponentModel;
using BackupManager.Domain.Enumerations;

namespace BackupManager.Domain.Interfaces
{
    public interface ISettingsBackupContent : INotifyPropertyChanged
    {
        ContentType ContentType { get; set; }
        string FullPath { get; set; }
        string Password { get; set; }
        string User { get; set; }
    }
}