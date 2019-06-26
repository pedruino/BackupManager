using BackupManager.Domain.Interfaces;
using Prism.Mvvm;

namespace BackupManager.UI.ViewModels
{
    public class SettingsCustomerViewModel : BindableBase
    {
        private readonly ISettingsCustomer _settingsCustomer;

        public SettingsCustomerViewModel(ISettingsCustomer settingsCustomer)
        {
            _settingsCustomer = settingsCustomer;
        }

        public string Id
        {
            get => _settingsCustomer.Id;
            set => _settingsCustomer.Id = value;
        }

        public string Name
        {
            get => _settingsCustomer.Name;
            set => _settingsCustomer.Name = value;
        }
    }
}