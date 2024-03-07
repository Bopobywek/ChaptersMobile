using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services;
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
        public ObservableCollection<ObservableBook> Books { get; } = new();
        public ReadingListViewModel(AuthorizationService authorizationService) : base(authorizationService)
        {
            var chapter = new Chapter { Title = "1231" };
            var book = new ObservableBook { Title = "Illuzion", Author = "Evgeniy" };
            book.Chapters.Add(chapter);

            Books.Add(book);
            Books.Add(book);
            Books.Add(book);
            Books.Add(book);
            authorizationService.AuthorizationChanged += base.Update;
        }
    }
}
