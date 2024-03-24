using ChaptersMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Results
{
    public sealed record GetBooksResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Rating { get; set; }
        public string? Cover { get; set; }
        [JsonPropertyName("bookStatus")]
        public BookStatus BookStatus { get; set; }
        [JsonPropertyName("userRating")]
        public int UserRating { get; set; }
    }
}
