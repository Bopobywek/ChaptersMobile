using ChaptersMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public class ViewBooksViewModel
    {
        public ObservableCollection<Book> BookList { get; } = new();


    }
}
