using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChaptersMobileApp.ViewModels
{
    public partial class RegistrationViewModel : AuthorizedViewModel
    {
        private readonly HttpClient _httpClient;

        // Add properties for other registration fields (e.g., Password, Email)

        public ICommand RegisterCommand { get; }

        public RegistrationViewModel(HttpClient httpClient)
        {
            RegisterCommand = new AsyncRelayCommand(Register);
            _httpClient = httpClient;
        }

        private async Task Register()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "localhost:8080/api/register");
            await _httpClient.SendAsync(request);
        }
    }
}
