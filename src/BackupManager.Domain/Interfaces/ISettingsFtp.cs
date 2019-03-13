using System.ComponentModel;

namespace BackupManager.Domain.Interfaces
{
    public interface ISettingsFtp : INotifyPropertyChanged
    {
        string Host { get; set; }
        string Password { get; set; }
        string Username { get; set; }
    }
}