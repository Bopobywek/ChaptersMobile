using ChaptersMobileApp.Services.Interfaces;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Font = Microsoft.Maui.Font;

namespace ChaptersMobileApp.Services
{
    public class AlertService : IAlertService
    {
        public async Task ShowSnackbar(string text, Color color = default!)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = color ?? Colors.Red,
                TextColor = Colors.White,
                ActionButtonTextColor = Colors.Yellow,
                CornerRadius = new CornerRadius(10),
                Font = Font.SystemFontOfSize(14),
                ActionButtonFont = Font.SystemFontOfSize(14)
            };

            TimeSpan duration = TimeSpan.FromSeconds(3);

            var snackbar = Snackbar.Make(text, null, "", duration, snackbarOptions, Shell.Current);

            await snackbar.Show(cancellationTokenSource.Token);
        }

        public async Task DisplayAlert(string text) {
            await Application.Current.MainPage.DisplayAlert("Ошибка", text, "OK");
        }

        public async Task<object?> ShowPopup(Popup popup)
        {
            return await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }
    }
}
