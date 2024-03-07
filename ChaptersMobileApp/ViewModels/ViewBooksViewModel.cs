using ChaptersMobileApp.Models;
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
    public partial class ViewBooksViewModel : ObservableObject, IQueryAttributable
    {
        public ObservableCollection<Book> BookList { get; } = new();

        [RelayCommand]
        public async Task ViewBook(Book book)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "book", book }
            };

            await Shell.Current.GoToAsync("bookView", navigationParameter);
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (!query.TryGetValue("BookList", out object booksList))
            {
                query.Clear();
                await Shell.Current.GoToAsync("..");
                return;
            }

            var list = (List<Book>)booksList;
            BookList.Clear();
            foreach (var item in list)
            {
                BookList.Add(item);
            }
            query.Clear();
        }
    }
}
