using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public partial class BookViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private int _bookId;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _author;

        [ObservableProperty]
        private double _rating;

        [ObservableProperty]
        private double _userRating;

        [ObservableProperty]
        private string? _cover;
        private readonly IWebApiService _webApiService;

        public ObservableCollection<Chapter> Chapters { get; } = new();
        public ObservableCollection<Review> Reviews { get; } = new();
        public BookViewModel(IWebApiService webApiService)
        {
            _webApiService = webApiService;
        }

        [RelayCommand]
        public async Task ReadChapter(Chapter chapter)
        {
            if (!chapter.IsRead)
            {
                _webApiService.MarkChapter(chapter.Id);
            }
            else
            {
                _webApiService.UnmarkChapter(chapter.Id);
            }

            chapter.IsRead = !chapter.IsRead;
        }

        [RelayCommand]
        public async Task ViewChapter(Chapter chapter)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "chapterId", chapter.Id }
            };
            await Shell.Current.GoToAsync("viewChapter", navigationParameter);
        }

        [RelayCommand]
        public async Task WriteReview(int bookId)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "bookId", bookId }
            };

            await Shell.Current.GoToAsync("writeReview", navigationParameter);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count == 0)
            {
                return;
            }

            if (query.TryGetValue("return", out _))
            {
                query.Clear();
                await Update();
                return;
            }

            if (!query.TryGetValue("book", out object bookObj))
            {
                query.Clear();
                await Shell.Current.GoToAsync("..");
            }

            var book = (Book)bookObj;
            Title = book.Title;
            Author = book.Author;
            Rating = book.Rating;
            UserRating = book.UserRating;
            Cover = book.Cover;
            BookId = book.Id;
            
            await Update();
            query.Clear();
        }

        private async Task Update()
        {
            var chapters = await _webApiService.GetChapters(BookId);
            foreach (var chapter in chapters)
            {
                Chapters.Add(
                    new()
                    {
                        Id = chapter.Id,
                        Title = chapter.Title.Trim(),
                        IsRead = chapter.IsRead,
                        UserRating = chapter.UserRating,
                    }
                );
            }

            var reviews = await _webApiService.GetReviews(BookId);
            foreach (var review in reviews)
            {
                Reviews.Add(
                    new()
                    {
                        Id = review.Id,
                        Title = review.Title.Trim(),
                        Text = review.Text,
                        AuthorBookRating = review.AuthorBookRating,
                        Rating = review.Rating,
                        AuthorUsername = review.AuthorUsername
                    }
                );
            }
        }
    }
}
