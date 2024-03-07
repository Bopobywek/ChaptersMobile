using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services;
using ChaptersMobileApp.Services.Interfaces;
using ChaptersMobileApp.Services.Results;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Reflection.Metadata.BlobBuilder;

namespace ChaptersMobileApp.ViewModels
{
    public partial class ProfileViewModel : AuthorizedViewModel
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private int _readingCount;
        public ObservableCollection<Book> ReadingBooks { get; } = new();

        [ObservableProperty]
        private int _willReadCount;
        public ObservableCollection<Book> WillReadBooks { get; } = new();

        [ObservableProperty]
        private int _readCount;
        public ObservableCollection<Book> ReadBooks { get; } = new();

        [ObservableProperty]
        private int _stopReadingCount;
        public ObservableCollection<Book> StopReadingBooks { get; } = new();

        private readonly IWebApiService _webApiService;

        public ICommand ViewBooksCommand { get; }

        public ProfileViewModel(AuthorizationService authorizationService, IWebApiService webApiService) : base(authorizationService)
        { 
            UpdateCommand = new RelayCommand(Update);
            ViewBooksCommand = new AsyncRelayCommand(ViewBooks);
            _webApiService = webApiService;
        }

        protected override void Update()
        { 
            base.Update();

            var username = SecureStorage.GetAsync("username").Result;
            Username = username;

            Task.Run(async () => await UpdateBooks());
        }

        private async Task ViewBooks()
        { 
        }

        private async Task UpdateBooks()
        {
            var books = await _webApiService.GetBooks(BookStatus.Reading);
            var entites = MapEntities(books);
            ReadingBooks.Clear();
            foreach (var entity in entites)
            {
                ReadingBooks.Add(entity);
            }
            ReadingCount = entites.Count;

            books = await _webApiService.GetBooks(BookStatus.WillRead);
            entites = MapEntities(books);
            WillReadBooks.Clear();
            foreach (var entity in entites)
            {
                WillReadBooks.Add(entity);
            }
            WillReadCount = entites.Count;

            books = await _webApiService.GetBooks(BookStatus.Finished);
            entites = MapEntities(books);
            ReadBooks.Clear();
            foreach (var entity in entites)
            {
                ReadBooks.Add(entity);
            }
            ReadCount = entites.Count;

            books = await _webApiService.GetBooks(BookStatus.StopReading);
            entites = MapEntities(books);
            StopReadingBooks.Clear();
            foreach (var entity in entites)
            {
                StopReadingBooks.Add(entity);
            }
            StopReadingCount = entites.Count;
        }

        private List<Book> MapEntities(IEnumerable<GetBooksResult> result)
        {
            return result.Select((x, index) => new Book { Title = x.Title, Author = x.Author, Rating = x.Rating, Position = index + 1, BookStatus = x.BookStatus, Cover = x.Cover, UserRating = x.UserRating }).ToList();

        }

    }
}
