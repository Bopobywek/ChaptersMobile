using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public partial class ActivityViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IWebApiService webApiService;

        public ObservableCollection<ActivityGroup> Activities { get; } = new();

        public ActivityViewModel(IWebApiService webApiService)
        {
            this.webApiService = webApiService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var username = (string)query["username"];
            query.Clear();
            var activities = await webApiService.GetUserActivities(username);
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
