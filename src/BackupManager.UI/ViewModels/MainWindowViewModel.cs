using BackupManager.Domain.Enumerations;
using BackupManager.Domain.Interfaces;
using BackupManager.Domain.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Input;

namespace BackupManager.UI.ViewModels
{
    public sealed class MainWindowViewModel : BindableBase
    {
        private readonly ApplicationService _applicationService;
        private readonly IAuthenticationService _authenticationService;
        private AccountViewModel _accountVM;
        private BindableBase _currentViewModel;

        public MainWindowViewModel(IAuthenticationService authenticationService, ApplicationService applicationService)
        {
            _authenticationService = authenticationService;
            _applicationService = applicationService;

            if (!IsLoggedAccount()) return;
        }

        public AccountViewModel AccountVM
        {
            get => _accountVM;
            set => SetProperty(ref _accountVM, value);
        }

        public ApplicationState ApplicationState
        {
            get => _applicationService.ApplicationServiceState;
            set
            {
                _applicationService.ApplicationServiceState = value;
                RaisePropertyChanged();
            }
        }

        public BindableBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel != null)
                {
                    _currentViewModel.PropertyChanged -= CurrentViewModel_PropertyChanged;
                }

                SetProperty(ref _currentViewModel, value);

                if (_currentViewModel != null)
                {
                    _currentViewModel.PropertyChanged += CurrentViewModel_PropertyChanged;
                }
            }
        }

        public DateTime LastSync => DateTime.Now;

        public ICommand ResumePauseCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ApplicationState = ApplicationState.Equals(ApplicationState.Paused) ? ApplicationState.Running : ApplicationState.Paused;
                });
            }
        }

        public ICommand ShowPreferencesCommand
        {
            get
            {
                return new DelegateCommand<object>((windowToOpen) =>
                {
                    if (windowToOpen is Window window)
                    {
                        window.Show();
                    }
                });
            }
        }

        public ICommand SignInCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CurrentViewModel = new AuthenticationViewModel(_authenticationService);
                });
            }
        }

        private void CurrentViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is AuthenticationViewModel authenticationViewModel)
            {
                switch (e.PropertyName)
                {
                    case nameof(AuthenticationViewModel.Customer):
                        {
                            AccountVM = new AccountViewModel(authenticationViewModel.Customer);
                            CurrentViewModel = AccountVM;
                        }
                        break;
                }
            }
        }

        private bool IsLoggedAccount()
        {
            //TODO: Implementar cookies/cache
            return _authenticationService.IsAuthorized() && false;
        }
    }
}