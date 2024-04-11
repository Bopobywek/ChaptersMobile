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
    public partial class CommentsViewModel : ObservableValidator, IQueryAttributable
    {
        private readonly IWebApiService webApiService;

        [ObservableProperty]
        private Comment _selectedComment = null;
        private string? username = null;

        public ObservableCollection<Comment> Comments { get; } = new();
        public CommentsViewModel(IWebApiService webApiService)
        {
            this.webApiService = webApiService;
        }

        [RelayCommand]
        public async Task OpenComment(Comment comment)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "chapterId", comment.ChapterId },
                { "bookId", comment.BookId },
                { "username", username }
            };
            await Shell.Current.GoToAsync("viewChapter", navigationParameter);
            SelectedComment = null;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            username = (string)query["username"];
            var comments = await webApiService.GetCommentsByUser(username);
            query.Clear();

            Comments.Clear();
            foreach (var comment in comments)
            {
                var book = await webApiService.GetBook(comment.BookId);
                var chapter = (await webApiService.GetChapters(comment.BookId)).Single(ch => ch.Id == comment.ChapterId);
                Comments.Add(
                    new Comment(comment.Id, comment.AuthorId, comment.AuthorUsername,
                    comment.Text, comment.Rating, comment.UserRating, comment.BookId, comment.ChapterId, $"К главе {chapter.Title} книги {book.Title}", comment.CreatedAt)
                );
            }
        }
    }
}
