using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Author {  get; set; }
        public double Rating { get; set; }
        public int Position { get; set; }
        public int UserRating { get; set; }
        public BookStatus BookStatus { get; set; }
        public string? Cover { get; set; }
    }
}
