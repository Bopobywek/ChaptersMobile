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
    public partial class BookViewModel : ObservableObject, IQueryAttributable
    {
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
        public BookViewModel(IWebApiService webApiService)
        {
            _webApiService = webApiService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
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

            var chapters = await _webApiService.GetChapters(book.Id);
            foreach (var chapter in chapters)
            {
                Chapters.Add(
                    new()
                    {
                        Title = chapter.Title,
                        IsRead = chapter.IsRead,
                        UserRating = chapter.UserRating,
                    }
                );
            }

            query.Clear();
        }
    }
}
