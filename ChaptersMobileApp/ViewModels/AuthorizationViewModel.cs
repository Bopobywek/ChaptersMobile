using ChaptersMobileApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public partial class AuthorizationViewModel : ObservableValidator
    {
        [Required(ErrorMessage = "Заполните поле \"Имя пользователя\"")]
        [MaxLength(255, ErrorMessage = "Слишком длинное имя")]
        [ObservableProperty]
        private string _username;

        [Required(ErrorMessage = "Заполните поле \"Пароль\"")]
        [ObservableProperty]
        private string _password;
        private readonly IAlertService _alertService;
        private readonly IWebApiService _apiService;

        public IRelayCommand AuthorizeCommand {  get; }
        public IRelayCommand RegisterCommand { get; }
        public IRelayCommand ReturnCommand { get; }
        public AuthorizationViewModel(IAlertService alertService, IWebApiService apiService) {
            AuthorizeCommand = new AsyncRelayCommand(Authorize);
            ReturnCommand = new AsyncRelayCommand(Return);
            _alertService = alertService;
            _apiService = apiService;
        }

        private async Task Authorize() { 
            ValidateAllProperties();
            if (HasErrors)
            {
                var message = string.Join('\n', GetErrors().Select(x => x.ErrorMessage));
                await _alertService.DisplayAlert(message);
            }

            var result = await _apiService.Authorize(_username, _password);
            if (result)
            {
                await SecureStorage.SetAsync("username", _username);
                await SecureStorage.SetAsync("password", _password);
                await Shell.Current.GoToAsync("..", true);
            } else
            {
                await _alertService.DisplayAlert("Неверный логин или пароль");
            }
        }

        private async Task Return() {
            await Shell.Current.GoToAsync("..", true);
        }


    }
}
