using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public partial class SubscribersListViewModel : ObservableValidator, IQueryAttributable
    {
        private string? username = null;
        private int? userId = null;
        public ObservableCollection<Subscription> Subscriptions { get; set; } = new ObservableCollection<Subscription>();
        private readonly IWebApiService webApiService;
        [ObservableProperty]
        private Subscription? _selectedSub = null;

        public SubscribersListViewModel(IWebApiService webApiService)
        {
            this.webApiService = webApiService;
        }

        [RelayCommand]
        public async Task OpenSub(Subscription subscription)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                var navigationParameter = new Dictionary<string, object>
            {
                { "username", subscription.Username },
                { "userId", subscription.UserId }
            };
                await Shell.Current.GoToAsync("profile", navigationParameter);
            });
        }

        [RelayCommand]
        public async Task Return()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "userId", userId },
                { "username", username }
            };
            await Shell.Current.GoToAsync("..", navigationParameter);
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            username = (string)query["username"];
            userId = (int?)query["userId"];

            var subscribers = await webApiService.GetSubscriptions(username!);
            Subscriptions.Clear();
            foreach (var subscriber in subscribers)
            {
                Subscriptions.Add(
                    new Subscription { Username = subscriber.Username, BooksCount = subscriber.NumberOfBooks }
                );
            }
        }
    }
}
