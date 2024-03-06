using ChaptersMobileApp.Services.Interfaces;
using ChaptersMobileApp.Services.Results;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChaptersMobileApp.ViewModels
{
    public partial class RegistrationViewModel : ObservableValidator
    {
        [Required(ErrorMessage = "Заполните поле \"Имя пользователя\"")]
        [MaxLength(255, ErrorMessage = "Слишком длинное имя")]
        [ObservableProperty]
        private string _username;

        [Required(ErrorMessage = "Заполните поле \"Пароль\"")]
        [ObservableProperty]
        private string _password;


        private readonly IWebApiService _apiService;
        private readonly IAlertService _alertService;

        // Add properties for other registration fields (e.g., Password, Email)

        public ICommand RegisterCommand { get; }
        public IRelayCommand ReturnCommand { get; }

        public RegistrationViewModel(IWebApiService apiService, IAlertService alertService)
        {
            RegisterCommand = new AsyncRelayCommand(Register);
            ReturnCommand = new AsyncRelayCommand(Return);
            _apiService = apiService;
            _alertService = alertService;
        }

        private async Task Register()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                var message = string.Join('\n', GetErrors().Select(x => x.ErrorMessage));
                await _alertService.DisplayAlert(message);
                return;
            }

            RegisterResult? result = null;
            try
            {
                result = await _apiService.Register(_username, _password);
            }
            catch (Exception ex)
            {
                await _alertService.DisplayAlert("Сервер недоступен");
                return;
            }

            if (result.IsSuccessStatusCode)
            {
                await _alertService.ShowSnackbar("Вы успешно зарегистрировались", Colors.Green);
                await SecureStorage.SetAsync("username", _username);
                await SecureStorage.SetAsync("password", _password);
                await Shell.Current.GoToAsync("../../", true);
            }
            else
            {
                await _alertService.DisplayAlert($"{result.StatusCode}: {result.Message}");
            }
        }

        private async Task Return()
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
