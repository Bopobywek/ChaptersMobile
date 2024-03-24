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
    public partial class ProfileViewModel : AuthorizedViewModel, IQueryAttributable
    {
        [ObservableProperty]
        private string? _username;
        private int? _userId;

        [ObservableProperty]
        private int _readingCount;

        [ObservableProperty]
        private bool _myAccount = true;

        [ObservableProperty]
        private bool _subscribed = false;
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
            if (Username is null || Username == username)
            {
                MyAccount = true;
                Username = username;

                Task.Run(async () => await UpdateBooks());
            }
            else 
            {
                MyAccount = false;
                Task.Run(async () => await UpdateBooks());
            }

        }

        [RelayCommand]
        private async Task ViewComments()
        {
            await Shell.Current.GoToAsync("comments");
        }

        [RelayCommand]
        private async Task Subscribe()
        {
            await _webApiService.Subscribe(_userId.Value);
            Subscribed = true;
        }

        [RelayCommand]
        private async Task Unsubscribe()
        {
            await _webApiService.Unsubscribe(_userId.Value);
            Subscribed = false;
        }

        [RelayCommand]
        private async Task OpenActivity()
        {
            await Shell.Current.GoToAsync("activities");
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
            var books = await _webApiService.GetBooks(BookStatus.Reading, _username);
            var entites = MapEntities(books);
            ReadingCount = FillList(ReadingBooks, ReadingBooksFull, entites);

            books = await _webApiService.GetBooks(BookStatus.WillRead, _username);
            entites = MapEntities(books);
            WillReadCount = FillList(WillReadBooks, WillReadBooksFull, entites);

            books = await _webApiService.GetBooks(BookStatus.Finished, _username);
            entites = MapEntities(books);
            ReadCount = FillList(ReadBooks, ReadBooksFull, entites);

            books = await _webApiService.GetBooks(BookStatus.StopReading, _username);
            entites = MapEntities(books);
            StopReadingCount = FillList(StopReadingBooks, StopReadingBooksFull, entites);

            var subs = await _webApiService.GetSubscriptions(await SecureStorage.GetAsync("username"));
            Subscribed = subs.Select(x => x.Username).Contains(Username);
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
            return result.Select((x, index) => new Book { Id = x.Id, Title = x.Title, Author = x.Author, Rating = x.Rating, Position = index + 1, BookStatus = x.BookStatus, Cover = x.Cover, UserRating = x.UserRating }).ToList();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("username", out object username))
            {
                Username = (string)username;
                _userId = (int)query["userId"];
                Task.Run(async () => await UpdateBooks());
                query.Clear();
            }
        }
    }
}
