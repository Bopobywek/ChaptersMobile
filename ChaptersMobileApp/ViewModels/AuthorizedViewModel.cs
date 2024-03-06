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

        public ICommand AuthorizeCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand LogOutCommand { get; }

        public AuthorizedViewModel() { 
            var username = SecureStorage.Default.GetAsync("username").Result;
            Authorized = username is not null;
            AuthorizeCommand = new AsyncRelayCommand(Authorize);
            UpdateCommand = new RelayCommand(Update);
            LogOutCommand = new RelayCommand(LogOut);
        }

        private async Task Authorize()
        {
            await Shell.Current.GoToAsync("auth");
        }

        private void Update()
        {
            var username = SecureStorage.Default.GetAsync("username").Result;
            Authorized = username is not null;
        }

        private void LogOut()
        {
            var username = SecureStorage.Default.GetAsync("username").Result;
            Authorized = false;
        }

    }
}
