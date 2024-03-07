using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services;
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
        public ObservableCollection<Subscription> Subscriptions { get; } = new();
        public SubscriptionsViewModel(AuthorizationService authorizationService) : base(authorizationService)
        {
            var sub = new Subscription { Username = "golubex", BooksCount = 5 };
            var sub2 = new Subscription { Username = "Aragorn", BooksCount = 228 };

            Subscriptions.Add(sub);
            Subscriptions.Add(sub2);

            authorizationService.AuthorizationChanged += base.Update;
        }
    }
}
