using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services;
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
    public partial class RatingViewModel : ObservableValidator
    {
        [ObservableProperty]
        private bool _authorized = false;

        private readonly IWebApiService _webApiService;
        private readonly AuthorizationService _authorizationService;

        public ObservableCollection<Book> BookList { get; } = new();

        public RatingViewModel(IWebApiService webApiService, AuthorizationService authorizationService)
        {
            authorizationService.AuthorizationChanged += Update;
            Update();
            _webApiService = webApiService;
            _authorizationService = authorizationService;
        }

        private void Update()
        {
            Task.Run(async () =>
            {
                await UpdateBooks();
            });
        }

        private async Task UpdateBooks()
        {
            Authorized = (await SecureStorage.Default.GetAsync("username")) is not null;
            var books = await _webApiService.GetBooks();
            var entites = books.Select((x, index) => new Book { Title = x.Title, Author = x.Author, Rating = x.Rating, Position = index + 1, BookStatus = x.BookStatus, Cover = x.Cover}).ToList();
            BookList.Clear();
            foreach (var entity in entites)
            { 
                BookList.Add(entity);
            }
        }
    }
}
