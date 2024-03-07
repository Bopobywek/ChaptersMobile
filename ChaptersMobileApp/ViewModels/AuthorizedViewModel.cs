using ChaptersMobileApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChaptersMobileApp.ViewModels
{
    public partial class AuthorizedViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool authorized;
        private readonly AuthorizationService _authorizationNotifierService;

        public ICommand AuthorizeCommand { get; }
        public ICommand UpdateCommand { get; protected set; }
        public ICommand LogOutCommand { get; }

        public AuthorizedViewModel(AuthorizationService authorizationNotifierService) { 
            var username = SecureStorage.Default.GetAsync("username").Result;
            Authorized = username is not null;
            AuthorizeCommand = new AsyncRelayCommand(Authorize);
            UpdateCommand = new RelayCommand(Update);
            LogOutCommand = new RelayCommand(LogOut);
            _authorizationNotifierService = authorizationNotifierService;
        }

        private async Task Authorize()
        {
            await Shell.Current.GoToAsync("auth");
        }

        protected virtual void Update()
        {
            var username = SecureStorage.Default.GetAsync("username").Result;
            Authorized = username is not null;
           
        }

        private void LogOut()
        {
            _authorizationNotifierService.LogOut();
            Authorized = false;
        }

    }
}
