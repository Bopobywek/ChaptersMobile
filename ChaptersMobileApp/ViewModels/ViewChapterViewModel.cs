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
    public partial class ViewChapterViewModel : ObservableValidator, IQueryAttributable
    {
        private readonly IWebApiService _webApiService;

        public ObservableCollection<Comment> CommentList { get; } = new();

        [ObservableProperty]
        public string _commentText;

        public ViewChapterViewModel(IWebApiService webApiService)
        {
            _webApiService = webApiService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var chapterId = (int)query["chapterId"];

            var comments = await _webApiService.GetComments(chapterId);
            foreach (var comment in comments)
            {
                CommentList.Add(
                    new Comment(comment.Id,
                        comment.AuthorId,
                        comment.AuthorUsername,
                        comment.Text,
                        comment.Rating,
                        comment.UserRating,
                        comment.CreatedAt
                    )
                );
            }

            query.Clear();
        }
    }
}
