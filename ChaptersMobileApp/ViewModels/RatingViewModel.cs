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
    public class RatingViewModel : ObservableValidator
    {
        public ObservableCollection<Book> BookList { get; } = new();

        public RatingViewModel()
        {
            BookList.Add(new Book { 
                Title = "Whispers in the Wind",
                Author = "The Great Adventure",
                Rating = 9.02,
                Position = 1
            });
            BookList.Add(new Book
            {
                Title = "Whispers in the Wind",
                Author = "The Great Adventure",
                Rating = 9.02,
                Position = 2
            });
            BookList.Add(new Book
            {
                Title = "Whispers in the Wind",
                Author = "The Great Adventure",
                Rating = 9.02,
                Position = 3
            });
            BookList.Add(new Book
            {
                Title = "Whispers in the Wind",
                Author = "The Great Adventure",
                Rating = 9.02,
                Position = 4
            });
        }
    }
}
