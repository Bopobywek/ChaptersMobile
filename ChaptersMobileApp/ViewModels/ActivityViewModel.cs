using ChaptersMobileApp.Models;
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
        public ObservableCollection<ActivityGroup> Activities { get; } = new();

        public ActivityViewModel()
        {
            
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            query.Clear();
            Activities.Clear();
            Activities.Add(
                    new ActivityGroup(new DateTimeOffset(2024, 3, 17, 12, 37, 7, TimeSpan.FromHours(3)), new List<Activity>
                    {
                        new Activity(1, 6, "Bopobywek12345", "начинает читать книгу \"Иллюзион\"", new DateTimeOffset(2024, 3, 14, 16, 24, 7, TimeSpan.FromHours(3))),
                        new Activity(1, 6, "Bopobywek12345", "прочитывает главу \"Глава первая. Буря над стеклянным городом\" книги \"Иллюзион\"", new DateTimeOffset(2024, 3, 14, 16, 31, 7, TimeSpan.FromHours(3)))
                    }.OrderByDescending(x => x.CreatedAt).ToList())
            );

        }
    }
}
