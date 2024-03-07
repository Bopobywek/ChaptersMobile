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
        public List<Book> ReadingBooksFull { get; } = new();

        [ObservableProperty]
        private int _willReadCount;
        public ObservableCollection<Book> WillReadBooks { get; } = new();
        public List<Book> WillReadBooksFull { get; } = new();

        [ObservableProperty]
        private int _readCount;
        public ObservableCollection<Book> ReadBooks { get; } = new();
        public List<Book> ReadBooksFull { get; } = new();

        [ObservableProperty]
        private int _stopReadingCount;
        public ObservableCollection<Book> StopReadingBooks { get; } = new();
        public List<Book> StopReadingBooksFull { get; } = new();

        private readonly IWebApiService _webApiService;
        private readonly IAlertService _alertService;

        public ProfileViewModel(AuthorizationService authorizationService,
            IWebApiService webApiService,
            IAlertService alertService) : base(authorizationService)
        { 
            UpdateCommand = new RelayCommand(Update);
            _webApiService = webApiService;
            _alertService = alertService;
        }

        protected override void Update()
        { 
            base.Update();

            var username = SecureStorage.GetAsync("username").Result;
            Username = username;

            Task.Run(async () => await UpdateBooks());
        }

        [RelayCommand]
        private async Task ViewBooks(string type)
        {
            var navigationParameter = new Dictionary<string, object>
            {
            };
            var paramName = "BookList";
            switch (type)
            {
                case "Read":
                    navigationParameter.Add(paramName, ReadBooksFull);
                    break;
                case "StopReading":
                    navigationParameter.Add(paramName, StopReadingBooksFull);
                    break;
                case "WillRead":
                    navigationParameter.Add(paramName, WillReadBooksFull);
                    break;
                case "Reading":
                    navigationParameter.Add(paramName, ReadingBooksFull);
                    break;
            }
            if (((List<Book>)navigationParameter[paramName]).Count == 0)
            {
                await _alertService.ShowSnackbar("Список пуст", Colors.OrangeRed);
                return;
            }

            await Shell.Current.GoToAsync("viewBooks", navigationParameter);
        }

        private async Task UpdateBooks()
        {
            var books = await _webApiService.GetBooks(BookStatus.Reading);
            var entites = MapEntities(books);
            ReadingCount = FillList(ReadingBooks, ReadingBooksFull, entites);

            books = await _webApiService.GetBooks(BookStatus.WillRead);
            entites = MapEntities(books);
            WillReadCount = FillList(WillReadBooks, WillReadBooksFull, entites);

            books = await _webApiService.GetBooks(BookStatus.Finished);
            entites = MapEntities(books);
            ReadCount = FillList(ReadBooks, ReadBooksFull, entites);

            books = await _webApiService.GetBooks(BookStatus.StopReading);
            entites = MapEntities(books);
            StopReadingCount = FillList(StopReadingBooks, StopReadingBooksFull, entites);
        }

        private int FillList(ObservableCollection<Book> preview, List<Book> fullList, IEnumerable<Book> entities)
        {
            preview.Clear();
            fullList.Clear();
            foreach (var entity in entities)
            {
                if (preview.Count == 0)
                {
                    preview.Add(entity);
                }
                fullList.Add(entity);
            }
            return entities.Count();
        }

        private List<Book> MapEntities(IEnumerable<GetBooksResult> result)
        {
            return result.Select((x, index) => new Book { Title = x.Title, Author = x.Author, Rating = x.Rating, Position = index + 1, BookStatus = x.BookStatus, Cover = x.Cover, UserRating = x.UserRating }).ToList();

        }

    }
}
