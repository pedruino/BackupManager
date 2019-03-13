using System.ComponentModel;

namespace BackupManager.Domain.Interfaces
{
    public interface ISettingsCustomer: INotifyPropertyChanged
    {
        string Hash { get; }
        string Id { get; set; }
        string Name { get; set; }
    }
}