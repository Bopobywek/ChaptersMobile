using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services;
using ChaptersMobileApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public class SubscriptionsViewModel : AuthorizedViewModel
    {
        private readonly IWebApiService _webApiService;

        public ObservableCollection<Subscription> Subscriptions { get; } = new();
        public SubscriptionsViewModel(AuthorizationService authorizationService, IWebApiService webApiService) : base(authorizationService)
        {
            authorizationService.AuthorizationChanged += base.Update;
            _webApiService = webApiService;
            Task.Run(UpdateSubscriptions);
        }

        private async Task UpdateSubscriptions()
        {
            var subscribers = await _webApiService.GetSubscriptions();
            foreach (var subscriber in subscribers)
            {
                Subscriptions.Add(
                    new Subscription { Username = subscriber.Username, BooksCount = subscriber.NumberOfBooks }
                );
            }
        }
    }
}
