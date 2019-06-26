using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BackupManager.Domain.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;

namespace BackupManager.UI.ViewModels
{
    public class AuthenticationViewModel : BindableBase
    {
        private readonly IAuthenticationService _authenticationService;
        private ISettingsCustomer _customer;

        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public ISettingsCustomer Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }

        public ICommand LoginCommand
        {
            get
            {
                return new DelegateCommand<object>((passwordBox) =>
                {
                    if (!(passwordBox is PasswordBox securePassword)) return;

                    var user = _authenticationService.AuthenticateUser(Username, securePassword.Password).Result;
                    if (user == null)
                    {
                        MessageBox.Show("Invalid username or password!", "Authentication failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        Customer = user;
                    }
                });
            }
        }

        public string Username { get; set; }
    }
}