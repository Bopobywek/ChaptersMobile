using ChaptersMobileApp.Services;
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
        private readonly AuthorizationService _authorizationService;

        public IRelayCommand AuthorizeCommand {  get; }
        public IRelayCommand RegisterCommand { get; }
        public IRelayCommand ReturnCommand { get; }
        public AuthorizationViewModel(IAlertService alertService, IWebApiService apiService, AuthorizationService authorizationService) {
            AuthorizeCommand = new AsyncRelayCommand(Authorize);
            RegisterCommand = new AsyncRelayCommand(Register);
            ReturnCommand = new AsyncRelayCommand(Return);
            _alertService = alertService;
            _apiService = apiService;
            _authorizationService = authorizationService;
        }

        private async Task Authorize() { 
            ValidateAllProperties();
            if (HasErrors)
            {
                var message = string.Join('\n', GetErrors().Select(x => x.ErrorMessage));
                await _alertService.DisplayAlert(message);
                return;
            }

            bool result = false;
            try
            {
                 result = await _apiService.Authorize(_username, _password);
            } catch (Exception ex)
            {
                await _alertService.DisplayAlert("Сервер недоступен");
                return;
            }

            if (result)
            {
                await _authorizationService.Authorize(_username, _password);
                await Shell.Current.GoToAsync("..", true);
            } else
            {
                await _alertService.DisplayAlert("Неверный логин или пароль");
            }
        }

        private async Task Return() {
            await Shell.Current.GoToAsync("..", true);
        }

        private async Task Register() {
            await Shell.Current.GoToAsync("register", true);
        }


    }
}
