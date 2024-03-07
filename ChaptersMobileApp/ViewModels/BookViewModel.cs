using ChaptersMobileApp.Models;
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

        public ObservableCollection<Book> Chapters { get; } = new() 
        { 
            new Book { Title = "Глава 1. affdadfadfdafdafafda"}, 
            new Book { Title = "Глава 2. "}, 
            new Book { Title = "Глава 3. "}, 
            new Book { Title = "Глава 4. "}, 
            new Book { Title = "Глава 5. "}, 
            new Book { Title = "Глава 6. "}, 
            new Book { Title = "Глава 7. "}, 
            new Book { Title = "Глава 8. "}, 
            new Book { Title = "Глава 9. "} 
        };

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

            query.Clear();
        }
    }
}
