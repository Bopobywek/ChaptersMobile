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

namespace ChaptersMobileApp.ViewModels
{
    public partial class ReadingListViewModel : AuthorizedViewModel
    {
        private readonly IWebApiService _webApiService;

        [ObservableProperty]
        private ObservableBook? _selectedBook = null;

        private int? _chapterBookId = null;

        public ObservableCollection<ObservableBook> Books { get; } = new();

        public ReadingListViewModel(AuthorizationService authorizationService,
            IWebApiService webApiService) : base(authorizationService)
        {
            Update();
            authorizationService.AuthorizationChanged += base.Update;
            _webApiService = webApiService;
        }

        [RelayCommand]
        public async Task ReadChapter(Chapter chapter)
        {
            await _webApiService.MarkChapter(chapter.Id);
            foreach (var book in Books)
            {
                if (book.Chapters.Any(ch => ch.Id == chapter.Id))
                {
                    await UpdateBook(book.Id);
                    return;
                }
            }
        }

        [RelayCommand]
        public async Task OpenBook()
        {
            var item = SelectedBook;
            var bookApi = await _webApiService.GetBook(item.Id);
            var book = new Book
            {
                Id = bookApi.Id,
                Title = bookApi.Title,
                Author = bookApi.Author,
                BookStatus = bookApi.BookStatus,
                Cover = bookApi.Cover,
                Rating = bookApi.Rating,
                UserRating = bookApi.UserRating
            };
            var navigationParameter = new Dictionary<string, object>
            {
                { "book", book }
            };

            await Shell.Current.GoToAsync("bookView", navigationParameter);
            SelectedBook = null;
        }



        protected override void Update()
        {
            base.Update();

            Task.Run(async () => await UpdateBooks());
        }

        private async Task UpdateBooks()
        {
            while (await SecureStorage.GetAsync("username") is null)
            { 
            }

            var books = await _webApiService.GetBooks(BookStatus.Reading);
            await MapEntities(books);
        }

        private async Task UpdateBook(int bookId)
        {
            var book = Books.Single(x => x.Id ==  bookId);
            var chapters = await _webApiService.GetChapters(book.Id);
            book.Chapters.Clear();
            foreach (var chapter in chapters.Where(x => !x.IsRead).OrderBy(x => x.Number).Take(3))
            {
                book.Chapters.Add(new() { Id = chapter.Id, Title = chapter.Title.Trim(), IsRead = chapter.IsRead });
            }

        }
        private async Task MapEntities(IEnumerable<GetBooksResult> result)
        {
            Books.Clear();
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
                    entity.Chapters.Add(new() { Id = chapter.Id, Title = chapter.Title.Trim(), IsRead = chapter.IsRead });
                }

                Books.Add(entity);
            }
        }
    }
}
