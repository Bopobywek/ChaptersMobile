using ChaptersMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Results
{
    public sealed record GetBooksResult(
    int Id,
    string Title,
    string Author,
    double Rating,
    string? Cover,
    BookStatus BookStatus,
    int UserRating);
}
