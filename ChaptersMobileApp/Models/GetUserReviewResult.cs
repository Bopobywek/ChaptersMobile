namespace ChaptersMobileApp.Models;

public class GetUserReviewResult
{
    public int Id { get; init; }
    public int AuthorId { get; init; }
    public string AuthorUsername { get; init; }
    public int AuthorBookRating { get; init; }
    public string Title { get; init; }
    public string Text { get; init; }
    public int Rating { get; init; }
    public int UserRating { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public int BookId { get; init; }
    public string BookTitle { get; init; }
}