using BackupManager.Domain.Interfaces;
using BackupManager.UI.Properties;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace BackupManager.UI.ViewModels
{
    public class AccountViewModel : BindableBase
    {
        private readonly ISettingsCustomer _customer;

        public AccountViewModel(ISettingsCustomer customer)
        {
            _customer = customer;
        }

        public decimal AvailableSpace => new decimal(1000);

        public ICommand GoToBackupOnlineCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OpenUri(Resources.ContactWebsite);
                });
            }
        }

        public string Name => _customer.Name;

        public decimal UsedSpace => new decimal(0);

        public static bool IsValidUri(string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                return false;
            Uri tmp;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out tmp))
                return false;
            return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
        }

        public static bool OpenUri(string uri)
        {
            if (!IsValidUri(uri))
                return false;
            System.Diagnostics.Process.Start(uri);
            return true;
        }
    }
}