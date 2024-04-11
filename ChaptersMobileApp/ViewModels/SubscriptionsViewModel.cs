using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services;
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
    public partial class SubscriptionsViewModel : AuthorizedViewModel
    {
        private readonly IWebApiService _webApiService;
        [ObservableProperty]
        private Subscription? _selectedSub = null;

        public ObservableCollection<Subscription> Subscriptions { get; } = new();
        public ObservableCollection<ActivityGroup> Activities { get; } = new();
        public SubscriptionsViewModel(AuthorizationService authorizationService, IWebApiService webApiService) : base(authorizationService)
        {
            authorizationService.AuthorizationChanged += base.Update;
            _webApiService = webApiService;
            MainThread.InvokeOnMainThreadAsync(UpdateSubscriptions);
        }

        [RelayCommand]
        public async Task SearchUser(string text)
        {
            var users = await _webApiService.SearchUsers(text);
            Subscriptions.Clear();
            foreach (var subscriber in users)
            {
                Subscriptions.Add(
                    new Subscription { Username = subscriber.Username, BooksCount = subscriber.NumberOfBooks, UserId = subscriber.UserId }
                );
            }
        }

        protected override void Update()
        {
            base.Update();
            MainThread.InvokeOnMainThreadAsync(UpdateSubscriptions);
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

        public async Task UpdateSubscriptions()
        {
            var username = await SecureStorage.GetAsync("username");
            var subscribers = await _webApiService.GetSubscriptions(username!);
            Subscriptions.Clear();
            foreach (var subscriber in subscribers)
            {
                Subscriptions.Add(
                    new Subscription { Username = subscriber.Username, BooksCount = subscriber.NumberOfBooks }
                );
            }

            var activities = await _webApiService.GetSubscriptionsActivities();
            var groups = new Dictionary<DateOnly, List<Activity>>();
            foreach (var activity in activities)
            {
                var date = activity.CreatedAt.Date;
                var dateOnly = new DateOnly(date.Year, date.Month, date.Day);
                if (!groups.ContainsKey(dateOnly))
                {
                    groups[dateOnly] = new List<Activity>();
                }
                groups[dateOnly].Add(new Activity(activity.Id, activity.UserId, activity.Username, activity.Text, activity.CreatedAt));
            }
            Activities.Clear();
            foreach (var kv in groups.OrderByDescending(x => x.Key))
            {
                Activities.Add(
                    new ActivityGroup(new DateTimeOffset(kv.Key, new TimeOnly(0, 0), TimeSpan.Zero),
                    kv.Value
                    )
                );
            }
        }
    }
}
