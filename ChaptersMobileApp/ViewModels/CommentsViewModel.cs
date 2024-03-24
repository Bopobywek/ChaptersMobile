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
    public partial class CommentsViewModel : ObservableValidator, IQueryAttributable
    {
        public ObservableCollection<Comment> Comments { get; } = new();
        public CommentsViewModel()
        {

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            query.Clear();

            Comments.Clear();

            Comments.Add(
                new Comment(1, 1, "abc", "Очень интересная глава!", 10, 1, 0, 0, "К главе главе Театр Магии книги Иллюзион", DateTimeOffset.UtcNow.AddDays(-10)
                ));

            Comments.Add(
                new Comment(1, 1, "abc", "Круть!", 1, 1, 0, 0, "К главе Отражение книги Иллюзион", DateTimeOffset.UtcNow.AddDays(-10)
                ));
        }
    }
}
