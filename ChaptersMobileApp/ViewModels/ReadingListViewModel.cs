using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services;
using ChaptersMobileApp.Services.Interfaces;
using ChaptersMobileApp.Services.Results;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public partial class ReadingListViewModel : AuthorizedViewModel
    {
        private readonly IWebApiService _webApiService;

        public ObservableCollection<ObservableBook> Books { get; } = new();

        public ReadingListViewModel(AuthorizationService authorizationService,
            IWebApiService webApiService) : base(authorizationService)
        {
            Update();
            authorizationService.AuthorizationChanged += base.Update;
            _webApiService = webApiService;
        }

        [RelayCommand]
        public async Task ReadChapter()
        { 

        }



        protected override void Update()
        {
            base.Update();

            var username = SecureStorage.GetAsync("username").Result;

            Task.Run(async () => await UpdateBooks());
        }

        private async Task UpdateBooks()
        {
            Books.Clear();
            var books = await _webApiService.GetBooks(BookStatus.Reading);
            await MapEntities(books);
        }

        private async Task MapEntities(IEnumerable<GetBooksResult> result)
        {
            foreach (var book in result)
            {
                var entity = new ObservableBook {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Cover = book.Cover };
                var chapters = await _webApiService.GetChapters(book.Id);
                foreach (var chapter in chapters.Where(x => !x.IsRead).OrderBy(x => x.Number).Take(3))
                {
                    entity.Chapters.Add(new() { Title = chapter.Title, IsRead = chapter.IsRead });
                }

                Books.Add(entity);
            }
        }
    }
}
