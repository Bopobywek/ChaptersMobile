using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services.Interfaces;
using ChaptersMobileApp.Views;
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
        private string _bookStatus;
        private bool _isChapterRating = false;

        public ObservableCollection<string> Statuses { get; set; } = new ObservableCollection<string>
        {
            "Читаю",
            "Буду читать",
            "Прочитал",
            "Перестал читать"
        };

        [ObservableProperty]
        private string? _cover;
        private readonly IWebApiService _webApiService;
        private readonly IAlertService _alertService;

        public ObservableCollection<Chapter> Chapters { get; } = new();
        public ObservableCollection<Review> Reviews { get; } = new();
        public BookViewModel(IWebApiService webApiService, IAlertService alertService)
        {
            _webApiService = webApiService;
            _alertService = alertService;
            Task.Run(Update);
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
                { "chapterId", chapter.Id },
                { "title", chapter.Title }
            };
            await Shell.Current.GoToAsync("viewChapter", navigationParameter);
        }

        [RelayCommand]
        public async Task SelectStatus(string status)
        {
            var bookStatus = (BookStatus)(Statuses.IndexOf(status) + 1);
            await _webApiService.ChangeBookStatus(BookId, bookStatus);
        }

        [RelayCommand]
        public async Task OpenUser(Review review)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "username", review.AuthorUsername },
                { "userId", review.AuthorId }
            };
            await Shell.Current.GoToAsync("profile", navigationParameter);
        }

        [RelayCommand]
        public async Task OpenRating()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "currentRating", UserRating },
                { "itemId", BookId }
            };
            await Shell.Current.GoToAsync("rate", navigationParameter);
        }

        [RelayCommand]
        public async Task RateChapter(Chapter chapter)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "currentRating", chapter.Rating },
                { "itemId", chapter.Id }
            };
            _isChapterRating = true;
            await Shell.Current.GoToAsync("rate", navigationParameter);
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

        [RelayCommand]
        public async Task LikeReview(Review review)
        {
            await _webApiService.RateReview(review.Id, true);
            await Update();
        }

        [RelayCommand]
        public async Task DislikeReview(Review review)
        {
            await _webApiService.RateReview(review.Id, false);
            await Update();
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count == 0)
            {
                return;
            }

            if (query.TryGetValue("userRating", out object userRate))
            {
                var userRating = ((double)userRate);
                if (_isChapterRating)
                {
                    _webApiService.RateChapter((int)query["itemId"], (int)userRating);
                } else
                {
                    await _webApiService.RateBook(BookId, (int)userRating);
                    UserRating = userRating;
                }
                await Update();
                query.Clear();
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
            BookStatus = book.BookStatus is Models.BookStatus.NotStarted ? "" : Statuses[(int)book.BookStatus - 1];
            BookId = book.Id;
            
            await Update();
            query.Clear();
        }

        private async Task Update()
        {
            var bookResult = await _webApiService.GetBook(BookId);
            Rating = bookResult.Rating;

            var chapters = await _webApiService.GetChapters(BookId);
            Chapters.Clear();
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

            Reviews.Clear();
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
                        UserRating = review.UserRating,
                        AuthorId = review.AuthorId,
                        AuthorUsername = review.AuthorUsername
                    }
                );
            }
        }
    }
}
