using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Interfaces
{
    public interface IWebApiService
    {
        Task<bool> Authorize(string username, string password);
        Task<RegisterResult> Register(string username, string password);
        Task<List<GetBooksResult>> GetBooks(BookStatus? bookStatus = null);
        Task<List<GetChapterResult>> GetChapters(int bookId);
        Task<List<GetReviewResult>> GetReviews(int bookId);
        Task<bool> MarkChapter(int chapterId);
        Task<bool> UnmarkChapter(int chapterId);

    }
}
