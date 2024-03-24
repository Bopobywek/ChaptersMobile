using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services.Results;

using GetSubscriptionsResult = ChaptersMobileApp.Models.GetSubscriptionsResult;

namespace ChaptersMobileApp.Services.Interfaces
{
    public interface IWebApiService
    {
        Task<bool> Authorize(string username, string password);
        Task<RegisterResult> Register(string username, string password);
        Task<List<GetBooksResult>> GetBooks(BookStatus? bookStatus = null, string? username = null);
        Task<GetBooksResult?> GetBook(int bookId);
        Task<bool> ChangeBookStatus(int bookId, BookStatus bookStatus);
        Task<bool> RateBook(int bookId, int rating);
        Task<List<GetBooksResult>> SearchBooks(string query);
        Task<List<GetChapterResult>> GetChapters(int bookId);
        Task<bool> MarkChapter(int chapterId);
        Task<bool> UnmarkChapter(int chapterId);
        Task<bool> RateChapter(int chapterId, int rating);
        Task<List<GetReviewResult>> GetReviews(int bookId);
        Task<bool> PostReview(int bookId, string title, string text);
        Task<List<GetUserReviewResult>> GetReviewsByUser(string author);
        Task<bool> RateReview(int reviewId, bool rating);

        Task<List<GetCommentResult>> GetComments(int chapterId);
        Task<bool> PostComment(int chapterId, string text);
        Task<List<GetUserCommentResult>> GetCommentsByUser(string author);
        Task<bool> RateComment(int reviewId, bool rating);

        Task<List<GetSubscriptionsResult>> GetSubscriptions(string username);
        Task<bool> Subscribe(int userId);
        Task<bool> Unsubscribe(int userId);
        Task<List<GetUserActivityResult>> GetUserActivities(string username);
        Task<List<GetUserActivityResult>> GetSubscriptionsActivities();
    }
}
